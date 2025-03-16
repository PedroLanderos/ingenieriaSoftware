using ArticleApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArticleApi.Application.Interfaces
{
    public interface IArticle
    {
        Task<ArticleEntity> GetAllArticlesAsync();
        Task<ArticleEntity> GetArticleByIdAsync();
        Task<ArticleEntity> GetArticleByCriteria(Expression<Func<ArticleEntity, bool>> predicate);

    }
}
