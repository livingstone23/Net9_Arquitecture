using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Application.Validator;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Tranversal.Common;
using Pacagroup.Ecommerce.Tranversal.Logging;



namespace Pacagroup.Ecommerce.Application.IntegrationTest;



[TestClass]
public class AuthApplicationTest
{


    private Mock<IUsersDomain> _usersDomainMock = null!;
    private Mock<IJwtService> _jwtServiceMock = null!;
    private Mock<IAppLogger<AuthApplication>> _loggerMock = null!;
    private IMapper _mapper = null!;
    private SignUpDtoValidator _signUpDtoValidator = null!;
    private AuthApplication _sut = null!; // System Under Test



    [TestInitialize]
    public void Setup()
    {
        _usersDomainMock = new Mock<IUsersDomain>();
        _jwtServiceMock = new Mock<IJwtService>();
        _loggerMock = new Mock<IAppLogger<AuthApplication>>();

        // 👇 IMPORTANTE: usar el ctor con 2 argumentos (config + loggerFactory)
        var mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<SignUpDto, User>();
            },
            new NullLoggerFactory()
        );

        _mapper = mapperConfig.CreateMapper();
        _signUpDtoValidator = new SignUpDtoValidator();

        _sut = new AuthApplication(
            _usersDomainMock.Object,
            _jwtServiceMock.Object,
            _mapper,
            _loggerMock.Object,
            _signUpDtoValidator);
    }



    [TestMethod]
    public async Task SignInAsync_Should_Return_Error_When_Email_Does_Not_Exist()
    {
        // Arrange
        var dto = new SignInDto
        {
            Email = "no-existe@test.com",
            Password = "CualquierClave1!"
        };

        _usersDomainMock
            .Setup(x => x.GetByEmailAsync(dto.Email))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _sut.SignInAsync(dto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Data);
        Assert.AreEqual("Email no existe o no se encuentra registrado", result.Message);

        _loggerMock.Verify(
            x => x.LogError("Failed to validate email. Error: {message}", result.Message),
            Times.Once);
    }



    [TestMethod]
    public async Task SignInAsync_Should_Return_Error_When_Password_Is_Invalid()
    {
        // Arrange
        var dto = new SignInDto
        {
            Email = "user@test.com",
            Password = "ClaveIncorrecta"
        };

        var user = new User
        {
            Id = "1",
            Email = dto.Email,
            UserName = "user"
        };

        _usersDomainMock
            .Setup(x => x.GetByEmailAsync(dto.Email))
            .ReturnsAsync(user);

        _usersDomainMock
            .Setup(x => x.CheckPasswordAsync(user, dto.Password))
            .ReturnsAsync(false);

        // Act
        var result = await _sut.SignInAsync(dto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Data);
        Assert.AreEqual("Credenciales inválidas", result.Message);
    }



    [TestMethod]
    public async Task SignInAsync_Should_Return_Token_When_Credentials_Are_Valid()
    {
        // Arrange
        var dto = new SignInDto
        {
            Email = "user@test.com",
            Password = "ClaveCorrecta1!"
        };

        var user = new User
        {
            Id = "1",
            Email = dto.Email,
            UserName = "user"
        };

        _usersDomainMock
            .Setup(x => x.GetByEmailAsync(dto.Email))
            .ReturnsAsync(user);

        _usersDomainMock
            .Setup(x => x.CheckPasswordAsync(user, dto.Password))
            .ReturnsAsync(true);

        _jwtServiceMock
            .Setup(x => x.GenerateToken(user))
            .Returns("token-jwt-fake");

        // Act
        var result = await _sut.SignInAsync(dto);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual("Autenticación exitosa", result.Message);
        Assert.AreEqual("token-jwt-fake", result.Data.AccessToken);
        Assert.AreEqual("Bearer", result.Data.TokenType);
        Assert.AreEqual(3600, result.Data.ExpiresIn);
    }



    [TestMethod]
    public async Task SignUpAsync_Should_Return_Validation_Errors_When_Dto_Is_Invalid()
    {
        // Arrange: DTO vacío → rompe varias reglas del validator
        var dto = new SignUpDto();

        // Act
        var result = await _sut.SignUpAsync(dto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsFalse(result.Data);
        Assert.AreEqual("Errores de Validación", result.Message);
        Assert.IsNotNull(result.Errors);
    }



    [TestMethod]
    public async Task SignUpAsync_Should_Return_Error_When_User_Already_Exists()
    {
        // Arrange
        var dto = new SignUpDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "user@test.com",
            UserName = "user",
            Password = "Clave1234"
        };

        var existingUser = new User
        {
            Id = "1",
            Email = dto.Email,
            UserName = dto.UserName
        };

        _usersDomainMock
            .Setup(x => x.GetByEmailAsync(dto.Email))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _sut.SignUpAsync(dto);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsFalse(result.Data);
        Assert.AreEqual("El usuario ya existe", result.Message);
    }



    [TestMethod]
    public async Task SignUpAsync_Should_Create_User_When_Data_Is_Valid()
    {
        // Arrange
        var dto = new SignUpDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "nuevo@test.com",
            UserName = "nuevo",
            Password = "Clave1234"
        };

        _usersDomainMock
            .Setup(x => x.GetByEmailAsync(dto.Email))
            .ReturnsAsync((User?)null);

        _usersDomainMock
            .Setup(x => x.CreateUserAsync(It.IsAny<User>(), dto.Password))
            .ReturnsAsync(true);

        // Act
        var result = await _sut.SignUpAsync(dto);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(result.Data);
        Assert.AreEqual("Usuario creado exitosamente", result.Message);

        _usersDomainMock.Verify(
            x => x.CreateUserAsync(It.Is<User>(u =>
                    u.Email == dto.Email &&
                    u.UserName == dto.UserName),
                dto.Password),
            Times.Once);
    }

}
