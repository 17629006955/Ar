using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;
using System.Linq;
using System;

namespace Ar.Services
{
    public class ProductInfoService : IProductInfoService
    {
        public IList<ProductInfo> GetProductInfoList()
        {
            IList<ProductInfo> list = DapperSqlHelper.FindToList<ProductInfo>("select ProductCode,ProductName,ExperiencePrice,Imageurl from [dbo].[ProductInfo] where VersionEndTime is null", null, false);
            foreach(var p in list)
            {
                p.TypeShowList = GetTypeShow(p.ProductCode);
                p.GameCategoryList = GameCategoryShow(p.ProductCode);
            }
            return list;
        }

        public List<string> GetTypeShow(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@productCode", code, System.Data.DbType.String);
            string sql = @"SELECT b.ProductTypeName FROM ProductTypeList a, ProductType b WHERE a.ProductTypeCode= b.ProductTypeCode AND a.ProductCode= @productCode";
            IList<ProductType> list =DapperSqlHelper.FindToList<ProductType>(sql, paras, false);
            return list.Select(p => p.ProductTypeName).ToList();
        }
        public List<ListType> GameCategoryShow(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@productCode", code, System.Data.DbType.String);
            string sql = @"SELECT b.ListTypeCode,b.ListTypeName FROM ProductList a, List b WHERE a.ProductListCode= b.ProductListCode AND a.ProductCode= @productCode";
            IList<ListType> list = DapperSqlHelper.FindToList<ListType>(sql, paras, false);
            return list.ToList();
        }
        public ProductInfo GetProductInfo(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@productCode", code, System.Data.DbType.String);
            ProductInfo productInfo = DapperSqlHelper.FindOne<ProductInfo>("select  ProductCode,ProductName,ExperiencePrice,Imageurl,Instructions,Thriller,videourl,SpecialRequirements,ExperiencePopulation from [dbo].[ProductInfo] where ProductCode=@productCode and isnull(VersionEndTime,'9999-09-09')>getdate()", paras, false);
            productInfo.TypeShowList = GetTypeShow(productInfo.ProductCode);
            return productInfo;
        }

        public IList<ProductInfo> GetProductInfoListByListCode(string listCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@listCode", listCode, System.Data.DbType.String);
            IList<ProductInfo> list = DapperSqlHelper.FindToList<ProductInfo>(@"select a.ProductCode,a.ProductName,a.ExperiencePrice,a.Imageurl from [dbo].[ProductInfo] a ,[dbo].[ProductList] b where a.ProductCode=b.ProductCode and b.Status=1
              and b.ListCode=@listCode  and isnull(a.VersionEndTime,'9999-09-09')>getdate()", paras, false);
            foreach (var p in list)
            {
                p.TypeShowList = GetTypeShow(p.ProductCode);
            }
            return list;
        }

        /// <summary>
        /// 获取预定页面
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public PayPage GetPayPage(string productCode)
        {
            List<DateTime> date = new List<DateTime>();
            date.Add(DateTime.Now);
            date.Add(DateTime.Now.AddDays(1));
            date.Add(DateTime.Now.AddDays(2));
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@productCode", productCode, System.Data.DbType.String);
            PayPage productInfo = DapperSqlHelper.FindOne<PayPage>("select  ProductCode,ProductName,ExperiencePrice,Imageurl from [dbo].[ProductInfo] where ProductCode=@productCode and  isnull(VersionEndTime,'9999-09-09')>getdate()", paras, false);
            productInfo.Store = "";
            productInfo.PeopleCount = 1;
            productInfo.SelectDate = date;
            return productInfo;
        }
    }
}
