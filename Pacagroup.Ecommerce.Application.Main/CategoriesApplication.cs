using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Tranversal.Common;



namespace Pacagroup.Ecommerce.Application.Main;



/// <summary>
/// 
/// </summary>
public class CategoriesApplication: ICategoriesApplication
{
    
    private readonly ICategoriesDomain _categoriesDomain;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _distributedCache;
    
    public CategoriesApplication(ICategoriesDomain categoriesDomain, IMapper mapper, IDistributedCache distributedCache)
    {

        _categoriesDomain = categoriesDomain;
        _mapper = mapper;
        _distributedCache = distributedCache;

    }

    public async Task<Response<IEnumerable<CategoriesDto>>> GetAll()
    {

        var response = new Response<IEnumerable<CategoriesDto>>();

        var cacheKey = "categoriesList";

        try
        {

            ///Consulta en cache
            var redisCategories = await _distributedCache.GetAsync(cacheKey);
            if (redisCategories != null)
            {
                // Si existe en cache, deserializar y retornar
                var cachedCategories = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CategoriesDto>>(redisCategories);
                response.Data = cachedCategories;
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa desde Cache!!!";
                return response;
            }
            else
            {
                var categories = await _categoriesDomain.GetAll();
                response.Data = _mapper.Map<IEnumerable<CategoriesDto>>(categories);
                // Almacenar en cache
                var serializedCategories = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(response.Data);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)) // Expiración absoluta, tiempo de caducidad almacenada en cache
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5)); // Expiración deslizante, caduca si no se utiliza en periodo definido
                await _distributedCache.SetAsync(cacheKey, serializedCategories, options);

            }

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!!";
            }


        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }
        return response;

    }
}