using AutoMapper;
using Entities.Model;
using System;
using System.Collections.Generic;
using Top.Api;
using Top.Api.Domain;
using Top.Api.Request;
using Top.Api.Response;
using DotNet.Common;
using Entities;
using static Top.Api.Response.TbkDgItemCouponGetResponse;
using static Top.Api.Response.TbkItemInfoGetResponse;
using System.Linq;

namespace BLL.ThirtyPart
{
    public static class TaoBaoKe 
    {
        private static string pid = "&pid=mm_25162659_311500292_108899300176";
        static TaoBaoKe()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<NTbkItem, Items>();
                cfg.CreateMap<TbkCouponDomain, Items>();
            } 
           );
        }
        private static ITopClient GetClient()
        {
            ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest", "27442968", "61b34d292cd03e501497d2f227216329");
            return client;
        }

        /// <summary>
        /// taobao.tbk.dg.item.coupon.get( 好券清单API【导购】 )
        /// </summary>
        public static List<Items> QueryDgItemCoupon(PageInfo pageInfo,int cateId, string keyword,int platformId = 1)
        {
            List<Items> result = new List<Items>(100);
            try
            {
                var client = GetClient();
                TbkDgItemCouponGetRequest req = new TbkDgItemCouponGetRequest();
                req.AdzoneId = 108899300176;//pid: mm_25162659_311500292_108899300176的第三位
                req.Platform = platformId;// 1 淘宝，2 天猫
                if (cateId > 0)
                {
                    req.Cat = cateId.ToString();//后台类目ID，用,分割，最大10个，该ID可以通过taobao.itemcats.get接口获取到
                }
                else
                {
                    req.Q = keyword;
                }

                req.PageSize = pageInfo.PageSize;

                req.PageNo = pageInfo.PageIndex;//第几页，默认：1（当后台类目和查询词均不指定的时候，最多出10000个结果，即page_no*page_size不能超过10000；当指定类目或关键词的时候，则最多出100个结果）
                TbkDgItemCouponGetResponse rsp = client.Execute(req);

                var rsps = client.Execute(req);
                var configMap = new MapperConfiguration(
                       cfg => cfg.CreateMap<TbkCouponDomain, Items>()
                           .ForMember(dest => dest.TotalCount, m => m.MapFrom(src => src.CouponTotalCount))
                           .ForMember(dest => dest.SellPrice, m => m.MapFrom(src => src.ZkFinalPrice))
                           .ForMember(dest => dest.CommissionRate, m => m.MapFrom(src => src.CommissionRate))
                           .ForMember(dest => dest.PicUrl, m => m.MapFrom(src => src.PictUrl))
                           .ForMember(dest => dest.ShopName, m => m.MapFrom(src => src.ShopTitle))
                           .ForMember(dest => dest.BeginTime, m => m.MapFrom(src => src.CouponStartTime))
                           .ForMember(dest => dest.EndTime, m => m.MapFrom(src => src.CouponEndTime))
                           .ForMember(dest => dest.ClickUrl, m => m.MapFrom(src => src.CouponClickUrl))
                           .ForMember(dest => dest.ItemDesc, m => m.MapFrom(src => src.ItemDescription))
                           .ForMember(dest => dest.RemainCount, m => m.MapFrom(src => src.CouponRemainCount))
                           .ForMember(dest => dest.ItemInfo, m => m.MapFrom(src => src.CouponInfo))
                           .ForMember(dest => dest.ProductUrl, m => m.MapFrom(src => src.ItemUrl))
                           .ForMember(dest => dest.SmallImages, m => m.MapFrom(src => src.SmallImages.ToListString(',')))
                );
                var mapper = configMap.CreateMapper();

                if (rsp.TotalResults == 0)
                {
                    return new List<Items>();
                }


                foreach (var item in rsp.Results)
                {
                    var dest = mapper.Map<TbkCouponDomain, Items>(item);
                    dest.TypeId = 1;
                    dest.LastTime = DateTime.Now;
                    if (dest.Commission <= 0)
                    {
                        dest.Commission = dest.SellPrice * dest.CommissionRate / 100;
                    }
                    dest.CateId = dest.Category;
                    dest.OrdId = 1;
                    dest.IsShow = 1;
                    dest.AliId = platformId.ToString();
                    dest.Status = 1;
                    dest.Platform = platformId.ToString();
                    dest.ProductUrl += string.IsNullOrEmpty(dest.ProductUrl) ? "" : pid;
                    if (keyword != null && keyword.Length < 6)
                    {
                        dest.Tags = keyword;
                    }

                    if (!string.IsNullOrEmpty(dest.ItemInfo))
                    {
                        if (dest.ItemInfo.Contains("无条件"))
                        {
                            var temp = dest.ItemInfo.Split("元");
                            dest.YouhuiPrice = temp[0].TryToDecimal();
                        }
                        else if (dest.ItemInfo.Contains("减"))
                        {
                            var temp = dest.ItemInfo.Split("减");
                            dest.YouhuiPrice = temp[1].Replace("元", "").TryToDecimal();
                        }
                        dest.FinalPrice = dest.SellPrice - dest.YouhuiPrice;
                    }
                    result.Add(dest);
                    Console.WriteLine($"Title:{dest.Title},Rate:{dest.CommissionRate},Time:{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="platform">平台ID：链接形式：1：PC，2：无线，默认：１</param>
        /// <returns></returns>
        public static List<Items> QueryProductDetail(List<Items> req, int platform)
        {
            try
            {
                int pageSize = 35;
                int pageTotal = req.Count / pageSize + 1;
                if (platform == 0)
                {
                    platform = 1;
                }

                for (int i = 0; i < pageTotal; i++)
                {
                    var queryList = req.Skip(i * pageSize).Take(pageSize);
                    string ids = queryList.Select(q => q.NumIid).JoinToString();
                    var result = QueryItemDetailInfo(ids, platform);
                    if (result == null || result.Count == 0)
                    {
                        continue;
                    }

                    foreach (var item in result)
                    {
                        var temp = req.FirstOrDefault(q => q.NumIid == item.NumIid);
                        if (temp == null)
                        {
                            continue;
                        }
                        if (platform == 1)
                        {
                            temp.UserType = item.UserType;
                            temp.ShopDsr = item.ShopDsr;
                            temp.RateSum = item.RateSum;
                            temp.Price = item.Price;
                            temp.MaterialLibType = item.MaterialLibType;
                            temp.ProductUrl = item.ItemUrl + pid;
                            temp.IsPrepay = item.IsPrepay;
                            temp.IRfdRate = item.IRfdRate;
                            temp.HPayRate30 = item.HPayRate30;
                            temp.HGoodRate = item.HGoodRate;
                            temp.FreeShipment = item.FreeShipment;
                            if (item.CateId > 0)
                            {
                                temp.CateId = item.CateId;
                                temp.Category = item.Category;
                            }

                            if (temp.Volume > 10000)
                            {
                                temp.Tags += ",人气";
                            }

                            if (temp.FinalPrice == 9.9m && temp.FreeShipment)
                            {
                                temp.Tags += ",9.9";
                            }

                            if (temp.Volume > 100 && temp.YouhuiPrice > 200)
                            {
                                temp.Tags += ",特卖";
                            }

                            if (temp.Volume > 5000 && temp.YouhuiPrice > 100)
                            {
                                temp.Tags += ",精选";
                            }

                            if (temp.Volume > 30000 && temp.CommissionRate >= 10)
                            {
                                temp.Tags += ",推荐";
                            }

                            if (temp.Volume > 20000 && temp.CommissionRate >= 10)
                            {
                                temp.Tags += ",首页";
                            }

                            if (temp.FinalPrice <= 20m && temp.FreeShipment)
                            {
                                temp.Tags += ",20";
                            }

                            temp.CategoryName = item.CategoryName;
                            temp.CatLeafName = item.CatLeafName;
                            if (item.CategoryName != null)
                            {
                                temp.Tags += "," + item.CategoryName.Replace("/", ",");
                            }

                            if (item.CatLeafName != null)
                            {
                                temp.Tags += "," + temp.CatLeafName.Replace("/", ",");
                            }
                            if (temp.ItemUrl != null && temp.ItemUrl.Contains("pid") == false)
                            {
                                temp.ItemUrl = item.ItemUrl + pid;
                            }
                            temp.Tags = temp.Tags.Trim(',');
                        }
                        else if (platform == 2)
                        {
                            temp.ProductWapUrl = item.ItemUrl + pid;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            

            return req;
        }

        /// <summary>
        /// taobao.tbk.item.info.get( 淘宝客商品详情（简版） )
        /// doc:https://open.taobao.com/api.htm?docId=24518&docType=2
        /// </summary>
        public static List<Items> QueryItemDetailInfo(string numiids, int platform)
        {
            var client = GetClient();
            TbkItemInfoGetRequest req = new TbkItemInfoGetRequest();
            req.NumIids = numiids;//123,456商品Id,必须
             req.Platform = platform;//链接形式：1：PC，2：无线，默认：１
            //req.Ip = "11.22.33.43";
            var configMap = new MapperConfiguration(
                cfg => cfg.CreateMap<NTbkItemDomain, Items>()
                       .ForMember(dest => dest.Area, m => m.MapFrom(src => src.Provcity))
                       .ForMember(dest => dest.SellPrice, m => m.MapFrom(src => src.ZkFinalPrice))
                       .ForMember(dest => dest.PicUrl, m => m.MapFrom(src => src.PictUrl))
                       .ForMember(dest => dest.ShopName, m => m.MapFrom(src => src.Nick))
                       .ForMember(dest => dest.SmallImages, m => m.MapFrom(src => src.SmallImages.ToListString(',')))
                       .ForMember(dest => dest.Price, m => m.MapFrom(src => src.ReservePrice))
                       .ForMember(dest => dest.CategoryName, m => m.MapFrom(src => src.CatName))
              
            );
            var mapper = configMap.CreateMapper();
            List<Items> result = null;
            try
            {
                TbkItemInfoGetResponse rsp = client.Execute(req);
                if (rsp.Results.Count == 0)
                {
                    return null;
                }
                result = new List<Items>(rsp.Results.Count);
                foreach (var item in rsp.Results)
                {
                    var dest = mapper.Map<NTbkItemDomain, Items>(item);
                    dest.LastTime = DateTime.Now;
                    dest.Tags = item.CatName;
                    if (dest.Commission <= 0)
                    {
                        dest.Commission = dest.SellPrice * dest.CommissionRate / 100;
                    }
                    if (dest.Category > 0)
                    {
                        dest.CateId = dest.Category;
                    }
                  
                    dest.Status = 1;
                    dest.ProductUrl += string.IsNullOrEmpty(dest.ProductUrl) ? "" : pid;
                    result.Add(dest);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            

            return result;
        }

        /// <summary>
        /// taobao.tbk.item.get( 淘宝客商品查询 )
        /// doc:https://open.taobao.com/api.htm?docId=24515&docType=2
        /// </summary>
        public static List<Items> QueryItem(PageInfo page, string keyword)
        {
           var client = GetClient(); 
            TbkItemGetRequest req = new TbkItemGetRequest();

            req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick";
            req.Q = keyword;
            //req.Cat = "16,18";
            //req.Itemloc = "杭州";
            req.Sort = "total_sale_des";//排序_des（降序），排序_asc（升序），销量（total_sales），淘客佣金比率（tk_rate）， 累计推广量（tk_total_sales），总支出佣金（tk_total_commi）
            // req.IsTmall = false;
            //req.IsOverseas = false;
            //req.StartPrice = 10L;
            //req.EndPrice = 10L;
            //req.StartTkRate = 123L;
            //req.EndTkRate = 123L;
            // req.Platform = 1L;
            req.PageNo = page.PageIndex;
            req.PageSize = page.PageSize;
            TbkItemGetResponse rsp = client.Execute(req);

            var configMap = new MapperConfiguration(
                cfg => cfg.CreateMap<NTbkItem, Items>()
                       .ForMember(dest => dest.Area, m => m.MapFrom(src => src.Provcity))
                       .ForMember(dest => dest.SellPrice, m => m.MapFrom(src => src.ZkFinalPrice))
                       .ForMember(dest => dest.CommissionRate, m => m.MapFrom(src => src.TkRate))
                       .ForMember(dest => dest.PicUrl, m => m.MapFrom(src => src.PictUrl))
                       .ForMember(dest => dest.ShopName, m => m.MapFrom(src => src.ShopTitle))
                       .ForMember(dest => dest.SmallImages, m => m.MapFrom(src => src.SmallImages.ToListString(',')))
                       .ForMember(dest => dest.Price, m => m.MapFrom(src => src.ReservePrice))
            );
            var mapper = configMap.CreateMapper();

            if (rsp.TotalResults == 0)
            {
                return null;
            }

            List<Items> result = new List<Items>(rsp.Results.Count);
            foreach (var item in rsp.Results)
            {
                var dest = mapper.Map<NTbkItem, Items>(item);
                dest.LastTime = DateTime.Now;
                dest.TypeId = 2;
                dest.Tags = keyword;
                if (dest.Commission <= 0)
                {
                    dest.Commission = dest.SellPrice * dest.CommissionRate / 100;
                }
                dest.FinalPrice = dest.SellPrice;
                dest.CateId = dest.Category;
                dest.OrdId = 1;
                dest.IsShow = 1;
                dest.AliId = "1";
                dest.Status = 1;
                dest.ProductUrl += string.IsNullOrEmpty(dest.ProductUrl) ? "" : pid;
                result.Add(dest);
                Console.WriteLine(string.Format("Title:{0},Rate:{1},smallimages:{2}", dest.Title, dest.CommissionRate, dest.SmallImages));
            }

            return result;
        }
        /// <summary>
        /// taobao.tbk.item.recommend.get( 淘宝客商品关联推荐查询 )
        /// doc:https://open.taobao.com/api.htm?docId=24517&docType=2
        /// </summary>
        public static void QueryItemRecommend()
        {
            var client = GetClient();
            TbkItemRecommendGetRequest req = new TbkItemRecommendGetRequest();
            req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url";
            req.NumIid = 123L;//商品Id,必须
            // req.Count = 20L;
            //req.Platform = 1L;
            TbkItemRecommendGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.shop.get( 淘宝客店铺查询 )
        /// doc:https://open.taobao.com/api.htm?docId=24521&docType=2
        /// </summary>
        public static void QueryShop()
        {
            var client = GetClient();
          
            TbkShopGetRequest req = new TbkShopGetRequest();
            req.Fields = "user_id,shop_title,shop_type,seller_nick,pict_url,shop_url";
            req.Q = "女装";//查询词，必须
            req.Sort = "commission_rate_des";
            //req.IsTmall = false;
            //req.StartCredit = 1L;
            //req.EndCredit = 20L;
            //req.StartCommissionRate = 2000L;
            //req.EndCommissionRate = 123L;
            //req.StartTotalAction = 1L;
            //req.EndTotalAction = 100L;
            //req.StartAuctionCount = 123L;
            //req.EndAuctionCount = 200L;
            //req.Platform = 1L;
            //req.PageNo = 1L;
            //req.PageSize = 20L;
            TbkShopGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.shop.recommend.get( 淘宝客店铺关联推荐查询 )
        /// doc:https://open.taobao.com/api.htm?docId=24522&docType=2
        /// </summary>
        public static void QueryTbkShopRecommend()
        {
            var client = GetClient();
            TbkShopRecommendGetRequest req = new TbkShopRecommendGetRequest();
            req.Fields = "user_id,shop_title,shop_type,seller_nick,pict_url,shop_url";
            req.UserId = 123L;//卖家Id
            //req.Count = 20L;
            //req.Platform = 1L;
            TbkShopRecommendGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.uatm.favorites.item.get( 获取淘宝联盟选品库的宝贝信息 )
        /// doc:https://open.taobao.com/api.htm?docId=26619&docType=2
        /// </summary>
        public static void QueryUatmFavoritesItem()
        {
            var client = GetClient();
            TbkUatmFavoritesItemGetRequest req = new TbkUatmFavoritesItemGetRequest();
            req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick,shop_title,zk_final_price_wap,event_start_time,event_end_time,tk_rate,status,type";
            req.Platform = 1L;
            req.PageSize = 20L;
            //推广位id，需要在淘宝联盟后台创建；且属于appkey备案的媒体id（siteid），如何获取adzoneid，请参考，http://club.alimama.com/read-htm-tid-6333967.html?spm=0.0.0.0.msZnx5
            req.AdzoneId = 34567L;//推广位id，必须
            req.Unid = "3456";//自定义输入串，英文和数字组成，长度不能大于12个字符，区分不同的推广渠道
            req.FavoritesId = 10010L;//选品库的id
            req.PageNo = 2L;
            
            TbkUatmFavoritesItemGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }


        /// <summary>
        /// taobao.tbk.uatm.favorites.get( 获取淘宝联盟选品库列表 )
        /// </summary>
        public static void QueryUatmFavorites()
        {
            var client = GetClient();
            TbkUatmFavoritesGetRequest req = new TbkUatmFavoritesGetRequest();
            req.PageNo = 1L;
            req.PageSize = 20L;
            req.Fields = "favorites_title,favorites_id,type";
            req.Type = 1L;//默认值-1；选品库类型，1：普通选品组，2：高佣选品组，-1，同时输出所有类型的选品组
            TbkUatmFavoritesGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

/// <summary>
        /// taobao.tbk.ju.tqg.get( 淘抢购api )
        /// doc:https://open.taobao.com/api.htm?docId=27543&docType=2
        /// </summary>
        public static void QueryJuTqg()
        {
            var client = GetClient();
            TbkJuTqgGetRequest req = new TbkJuTqgGetRequest();
            //推广位id（推广位申请方式：http://club.alimama.com/read.php?spm=0.0.0.0.npQdST&tid=6306396&ds=1&page=1&toread=1）
            req.AdzoneId = 123L;
            req.Fields = "click_url,pic_url,reserve_price,zk_final_price,total_amount,sold_num,title,category_name,start_time,end_time";
            req.StartTime = DateTime.Parse("2016-08-09 09:00:00");//最早开团时间
            req.EndTime = DateTime.Parse("2016-08-09 16:00:00");//最晚开团时间
            req.PageNo = 1L;
            req.PageSize = 40L;
            TbkJuTqgGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.item.click.extract( 链接解析api )
        /// 响应参数:
        /// open_iid	String	AAEhb-fzAAgOrLpFIt9AhGdJ,AAEhb-fzAAgOrLpFIt9AhGdJ	商品混淆ID
        /// item_id String	123	商品ID
        /// </summary>
        //public static void QueryItemClickExtract()
        //{
        //    var client = GetClient();
        //    TbkItemClickExtractRequest req = new TbkItemClickExtractRequest();
        //    req.ClickUrl = "https://s.click.taobao.com/***";//长链接或短链接
        //    TbkItemClickExtractResponse rsp = client.Execute(req);
        //    Console.WriteLine(rsp.Body);
        //}



        /// <summary>
        /// taobao.tbk.item.guess.like( 淘宝客商品猜你喜欢 )
        /// </summary>
        //public static void QueryItemGuessLike()
        //{
        //    var client = GetClient();
        //    TbkItemGuessLikeRequest req = new TbkItemGuessLikeRequest();
        //    req.AdzoneId = 123L;
        //    req.UserNick = "abc";
        //    req.UserId = 123456L;
        //    req.Os = "ios";
        //    req.Idfa = "65A509BA-227C-49AC-91EC-DE6817E63B10";
        //    req.Imei = "641221321098757";
        //    req.ImeiMd5 = "115d1f360c48b490c3f02fc3e7111111";
        //    req.Ip = "106.11.34.15";
        //    req.Ua = "Mozilla/5.0";
        //    req.Apnm = "com.xxx";
        //    req.Net = "wifi";
        //    req.Mn = "iPhone7%2C2";
        //    req.PageNo = 1L;
        //    req.PageSize = 20L;
        //    TbkItemGuessLikeResponse rsp = client.Execute(req);
        //    Console.WriteLine(rsp.Body);
        //}


        /// <summary>
        /// taobao.tbk.coupon.get( 阿里妈妈推广券信息查询 )
        /// </summary>
        public static void QueryCoupon()
        {
            var client = GetClient();
            TbkCouponGetRequest req = new TbkCouponGetRequest();
            req.Me = "nfr%2BYTo2k1PX18gaNN%2BIPkIG2PadNYbBnwEsv6mRavWieOoOE3L9OdmbDSSyHbGxBAXjHpLKvZbL1320ML%2BCF5FRtW7N7yJ056Lgym4X01A%3D";//带券ID与商品ID的加密串
            req.ItemId = 123L;//商品ID
            req.ActivityId = "sdfwe3eefsdf";//券ID
            TbkCouponGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.tpwd.create( 淘宝客淘口令 )
        /// </summary>
        public static void GetTpwdCreate()
        {
            var client = GetClient();
            TbkTpwdCreateRequest req = new TbkTpwdCreateRequest();
            req.UserId = "123";//生成口令的淘宝用户ID
            req.Text = "长度大于5个字符";//口令弹框内容,Must
            req.Url = "https://uland.taobao.com/";//口令跳转目标页,Must
            req.Logo = "https://uland.taobao.com/";//口令弹框logoURL
            req.Ext = "{}";//扩展字段JSON格式
            TbkTpwdCreateResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.content.get( 淘客媒体内容输出 )
        /// </summary>
        public static void GetContent()
        {
            var client = GetClient();
            TbkContentGetRequest req = new TbkContentGetRequest();
            req.AdzoneId = 123L;//推广位
            req.Type = 1L;//内容类型，1:图文、2: 图集、3: 短视频
            req.BeforeTimestamp = 1491454244598L;//表示取这个时间点以前的数据，以毫秒为单位（出参中的last_timestamp是指本次返回的内容中最早一条的数据，下一次作为before_timestamp传过来，即可实现翻页）
            req.Count = 10L;//表示期望获取条数
            req.Cid = 2L;//类目
            req.ImageWidth = 300L;//图片宽度
            req.ImageHeight = 300L;//	图片高度
            req.ContentSet = 1L;//默认可不传,内容库类型:1 优质内容
            TbkContentGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }


        /// <summary>
        /// taobao.tbk.content.get( 淘客媒体内容输出 )
        /// </summary>
        public static void GetDgNewuserOrder()
        {
            var client = GetClient();
            TbkDgNewuserOrderGetRequest req = new TbkDgNewuserOrderGetRequest();
            req.PageSize = 20L;//页大小，默认20，1~100
            req.AdzoneId = 123L;//mm_xxx_xxx_xxx的第三位
            req.PageNo = 1L;
            req.StartTime = DateTime.Parse("2018-01-24 00:34:05");//开始时间，当活动为淘宝活动，表示最早注册时间；当活动为支付宝活动，表示最早绑定时间；当活动为天猫活动，表示最早领取红包时间
            req.EndTime = DateTime.Parse("2018-01-24 00:34:05");//结束时间，当活动为淘宝活动，表示最晚结束时间；当活动为支付宝活动，表示最晚绑定时间；当活动为天猫活动，表示最晚领取红包的时间
            req.ActivityId = "119013_2";//活动id， 活动名称与活动ID列表，请参见https://tbk.bbs.taobao.com/detail.html?appId=45301&postId=8599277
            TbkDgNewuserOrderGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }


        /// <summary>
        /// taobao.tbk.sc.newuser.order.get( 淘宝客新用户订单API--社交 )
        /// </summary>
        public static void GetScNewuserOrder(string sessionKey)
        {
            var client = GetClient();
            TbkScNewuserOrderGetRequest req = new TbkScNewuserOrderGetRequest();
            req.PageSize = 20L;
            req.AdzoneId = 123L;//m_xxx_xxx_xxx的第三位
            req.PageNo = 1L;
            req.SiteId = 123L;//mm_xxx_xxx_xxx的第二位
            req.ActivityId = "119013_2";//活动id， 现有活动id包括： 2月手淘拉新：119013_2 3月手淘拉新：119013_3 4月手淘拉新：119013_4 拉手机支付宝新用户_赚赏金：200000_5,Must
            req.EndTime = DateTime.Parse("2018-01-24 00:34:05");//结束时间，当活动为淘宝活动，表示最晚结束时间；当活动为支付宝活动，表示最晚绑定时间；当活动为天猫活动，表示最晚领取红包的时间
            req.StartTime = DateTime.Parse("2018-01-24 00:34:05");//开始时间，当活动为淘宝活动，表示最早注册时间；当活动为支付宝活动，表示最早绑定时间；当活动为天猫活动，表示最早领取红包时间
            TbkScNewuserOrderGetResponse rsp = client.Execute(req, sessionKey);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.dg.optimus.material( 淘宝客物料下行-导购 )
        /// </summary>
        public static void GetDgOptimusMaterial()
        {
            var client = GetClient();
            TbkDgOptimusMaterialRequest req = new TbkDgOptimusMaterialRequest();
            req.PageSize = 20L;
            req.AdzoneId = 123L;//mm_xxx_xxx_xxx的第三位,Must
            req.PageNo = 1L;//第几页，默认：1
            req.MaterialId = 123L;//官方的物料Id(详细物料id见：https://tbk.bbs.taobao.com/detail.html?appId=45301&postId=8576096)
            req.DeviceValue = "xxx";//智能匹配-设备号加密后的值（MD5加密需32位小写）
            req.DeviceEncrypt = "MD5";//智能匹配-设备号加密类型：MD5
            req.DeviceType = "IMEI";//智能匹配-设备号类型：IMEI，或者IDFA，或者UTDID（UTDID不支持MD5加密）
            req.ContentId = 323L;//内容专用-内容详情ID
            req.ContentSource = "xxx";//内容专用-内容渠道信息
            req.ItemId = 33243L;//商品ID，用于相似商品推荐
            TbkDgOptimusMaterialResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.dg.material.optional( 通用物料搜索API（导购） )
        /// </summary>
        public static void GetDgMaterialOptional()
        {
            var client = GetClient();
            TbkDgMaterialOptionalRequest req = new TbkDgMaterialOptionalRequest();
            req.StartDsr = 10L;
            req.PageSize = 20L;
            req.PageNo = 1L;
            req.Platform = 1L;
            req.EndTkRate = 1234L;
            req.StartTkRate = 1234L;
            req.EndPrice = 10L;
            req.StartPrice = 10L;
            req.IsOverseas = false;
            req.IsTmall = false;
            req.Sort = "tk_rate_des";
            req.Itemloc = "杭州";
            req.Cat = "16,18";
            req.Q = "女装";
            req.MaterialId = 2836L;
            req.HasCoupon = false;
            req.Ip = "13.2.33.4";
            req.AdzoneId = 12345678L;//mm_xxx_xxx_12345678三段式的最后一段数字,Must
            req.NeedFreeShipment = true;
            req.NeedPrepay = true;
            req.IncludePayRate30 = true;
            req.IncludeGoodRate = true;
            req.IncludeRfdRate = true;
            req.NpxLevel = 2L;
            req.EndKaTkRate = 1234L;
            req.StartKaTkRate = 1234L;
            req.DeviceEncrypt = "MD5";
            req.DeviceValue = "xxx";
            req.DeviceType = "IMEI";
            TbkDgMaterialOptionalResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.dg.newuser.order.sum( 拉新活动汇总API--导购 )
        /// </summary>
        public static void GetDgNewuserOrderSum()
        {
            var client = GetClient();
            TbkDgNewuserOrderSumRequest req = new TbkDgNewuserOrderSumRequest();
            req.PageSize = 20L;
            req.AdzoneId = 123L;//mm_xxx_xxx_xxx的第三位,Must
            req.PageNo = 1L;
            req.SiteId = 123L;//mm_xxx_xxx_xxx的第二位  
            req.ActivityId = "sxxx";//活动id， 活动名称与活动ID列表，请参见https://tbk.bbs.taobao.com/detail.html?appId=45301&postId=8599277,Must
            req.SettleMonth = "201807";
            TbkDgNewuserOrderSumResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.sc.newuser.order.sum( 拉新活动汇总API--社交 )
        /// </summary>
        public static void GetScNewuserOrderSum(string sessionKey)
        {
            var client = GetClient();
            TbkScNewuserOrderSumRequest req = new TbkScNewuserOrderSumRequest();
            req.PageSize = 20L;
            req.AdzoneId = 123L;
            req.PageNo = 1L;
            req.SiteId = 123L;
            req.ActivityId = "sxxx";
            req.SettleMonth = "201807";
            TbkScNewuserOrderSumResponse rsp = client.Execute(req, sessionKey);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.content.effect.get( 淘客媒体内容效果输出 )
        /// </summary>
        public static void GetContentEffect(string sessionKey)
        {
            var client = GetClient();
            TbkContentEffectGetRequest req = new TbkContentEffectGetRequest();
            TbkContentEffectGetRequest.ContentEffectPageOptionDomain obj1 = new TbkContentEffectGetRequest.ContentEffectPageOptionDomain();
            obj1.Time = "2018-04-02";//选择效果日期，如果不传即按日期倒序排
            obj1.PageNo = 1L;
            obj1.PageSize = 50L;
            obj1.Pid = "mm_1_1_1";//如若不传则不做为筛选条件
            obj1.ContentId = "1234";
            req.Option_ = obj1;
            TbkContentEffectGetResponse rsp = client.Execute(req, sessionKey);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.sc.optimus.material( 淘宝客擎天柱通用物料API - 社交 )
        /// </summary>
        public static void GetScOptimusMaterial(string sessionKey)
        {
            var client = GetClient();
            TbkScOptimusMaterialRequest req = new TbkScOptimusMaterialRequest();
            req.PageSize = 20L;
            req.AdzoneId = 123L;//mm_xxx_xxx_xxx的第三位,Must
            req.PageNo = 1L;
            req.MaterialId = 123L;
            req.SiteId = 111L;//mm_xxx_xxx_xxx的第二位,Must
            req.DeviceType = "IMEI";
            req.DeviceEncrypt = "MD5";
            req.DeviceValue = "xxx";
            req.ContentId = 323L;
            req.ContentSource = "xxx";
            req.ItemId = 33243L;
            TbkScOptimusMaterialResponse rsp = client.Execute(req, sessionKey);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.dg.vegas.tlj.create( 淘礼金创建 )
        /// </summary>
        //public static void GetDgVegasTljCreate()
        //{
        //    var client = GetClient();
        //    TbkDgVegasTljCreateRequest req = new TbkDgVegasTljCreateRequest();
        //    req.CampaignType = "定向：DX；鹊桥：LINK_EVENT；营销：MKT";
        //    req.AdzoneId = 1234567L;//妈妈广告位Id,Must
        //    req.ItemId = 1234567L;//宝贝id,Must
        //    req.TotalNum = 10L;//淘礼金总个数,Must
        //    req.Name = "淘礼金来啦";//淘礼金名称，最大10个字符,Must
        //    req.UserTotalWinNumLimit = 1L;//单用户累计中奖次数上限,Must
        //    req.SecuritySwitch = true;// 启动安全：true；不启用安全：false;
        //    req.PerFace = "10";//单个淘礼金面额，支持两位小数，单位元,Must
        //    req.SendStartTime = DateTime.Parse("2018-09-01 00:00:00");//发放开始时间,Must
        //    req.SendEndTime = DateTime.Parse("2018-09-01 00:00:00");//发放截止时间
        //    req.UseEndTime = "5";//使用结束日期。如果是结束时间模式为相对时间，时间格式为1-7直接的整数, 例如，1（相对领取时间1天）； 如果是绝对时间，格式为yyyy-MM-dd，例如，2019-01-29，表示到2019-01-29 23:59:59结束
        //    req.UseEndTimeMode = 1L;//结束日期的模式,1:相对时间，2:绝对时间
        //    req.UseStartTime = "2019-01-29";//使用开始日期。相对时间，无需填写，以用户领取时间作为使用开始时间。绝对时间，格式 yyyy-MM-dd，例如，2019-01-29，表示从2019-01-29 00:00:00 开始
        //    TbkDgVegasTljCreateResponse rsp = client.Execute(req);
        //    Console.WriteLine(rsp.Body);
        //}

        /// <summary>
        /// taobao.tbk.activitylink.get( 淘宝联盟官方活动推广API-媒体 )
        /// </summary>
        public static void GetDgVegasTljCreate()
        {
            var client = GetClient();
            TbkActivitylinkGetRequest req = new TbkActivitylinkGetRequest();
            req.Platform = 1L;
            req.UnionId = "demo";
            req.AdzoneId = 123L;//推广位id，mm_xx_xx_xx pid三段式中的第三段。adzone_id需属于appKey拥有者,Must
            req.PromotionSceneId = 12345678L;//官方活动ID，从官方活动页获取,Must
            req.SubPid = "mm_123_123_123";//媒体平台下达人的淘客pid
            req.RelationId = "23";
            TbkActivitylinkGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

        /// <summary>
        /// taobao.tbk.sc.activitylink.toolget( 淘宝联盟官方活动推广API-工具 )
        /// </summary>
        //public static void GetScActivitylinkTool()
        //{
        //    var client = GetClient();
        //    TbkScActivitylinkToolgetRequest req = new TbkScActivitylinkToolgetRequest();
        //    req.AdzoneId = 123L;//推广位id，mm_xx_xx_xx pid三段式中的第三段,Must
        //    req.SiteId = 456L;//推广位id，mm_xx_xx_xx pid三段式中的第二段,Must
        //    req.Platform = 1L;
        //    req.UnionId = "demo";
        //    req.RelationId = "23";
        //    req.PromotionSceneId = 12345678L;//官方活动ID，从官方活动页获取,Must
        //    TbkScActivitylinkToolgetResponse rsp = client.Execute(req, sessionKey);
        //    Console.WriteLine(rsp.Body);
        //}

        /// <summary>
        /// taobao.tbk.dg.punish.order.get( 处罚订单查询 -导购-私域用户管理专用 )
        /// </summary>
        //public static void GetDgPunishOrder()
        //{
        //    var client = GetClient();
        //    TbkDgPunishOrderGetRequest req = new TbkDgPunishOrderGetRequest();
        //    TbkDgPunishOrderGetRequest.TopApiAfOrderOptionDomain obj1 = new TbkDgPunishOrderGetRequest.TopApiAfOrderOptionDomain();
        //    obj1.Span = 1200L;
        //    obj1.RelationId = 2222L;//渠道关系id
        //    obj1.TbTradeId = 258897956183171983L;//子订单号
        //    obj1.TbTradeParentId = 258897956183171983L;//父订单号
        //    obj1.PageSize = 1L;
        //    obj1.PageNo = 10L;
        //    obj1.StartTime = DateTime.Parse("2018 - 11 - 11 00:01:01");
        //    obj1.SpecialId = 23132L;
        //    obj1.ViolationType = 1L;
        //    obj1.PunishStatus = 1L;
        //    req.AfOrderOption_ = obj1;
        //    TbkDgPunishOrderGetResponse rsp = client.Execute(req);
        //    Console.WriteLine(rsp.Body);
        //}

        /// <summary>
        /// taobao.tbk.dg.vegas.tlj.instance.report( 导购淘礼金实例维度报表 )
        /// </summary>
        //public static void GetDgVegasTljInstanceReport()
        //{
        //    var client = GetClient();
        //    TbkDgVegasTljInstanceReportRequest req = new TbkDgVegasTljInstanceReportRequest();
        //    req.RightsId = "UZvYlKXRdTIBf%2B%2F%2FIV9MGw%3D%3D";//实例ID,Must
        //    TbkDgVegasTljInstanceReportResponse rsp = client.Execute(req);
        //    Console.WriteLine(rsp.Body);
        //}
    }
}
