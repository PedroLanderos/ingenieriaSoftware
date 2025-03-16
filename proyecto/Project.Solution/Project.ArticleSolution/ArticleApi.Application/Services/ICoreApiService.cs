using ArticleApi.Domain.Entities;
using Llaveremos.SharedLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleApi.Application.Services
{
    //interfaz que define como funcionara el servicio para obtener todos los campos de un articulo y la url del articulo
    public interface ICoreApiService
    {
        Task<List<ArticleEntity>> SearchArticles(string query, int page, int pageSieze);
        Task<Response> GetPdfUrlAsync(int articleId);
    }
}
