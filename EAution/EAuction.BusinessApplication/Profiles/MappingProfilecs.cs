using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EAuction.BusinessApplication.DTOs;
using EAuction.BusinessApplication.DTOs.Buyer;
using EAuction.BusinessApplication.DTOs.Seller;
using EAuction.Domain;


namespace EAuction.BusinessApplication.Profiles
{
    public class MappingProfilecs : Profile
    {
        public MappingProfilecs()
        {
           CreateMap<CreateProductDto, Product>().ForMember(dset => dset.SellerInfo.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.SellerInfo.LastName, opt => opt.MapFrom(src => src.LastName))
                 .ForMember(dest => dest.SellerInfo.Address, opt => opt.MapFrom(src => src.Address))
                 .ForMember(dest => dest.SellerInfo.City, opt => opt.MapFrom(src => src.City))
                 .ForMember(dest => dest.SellerInfo.State, opt => opt.MapFrom(src => src.State))
                 .ForMember(dest => dest.SellerInfo.Pin, opt => opt.MapFrom(src => src.Pin))
                 .ForMember(dest => dest.SellerInfo.Phone, opt => opt.MapFrom(src => src.Phone))
                 .ForMember(dest => dest.SellerInfo.Email, opt => opt.MapFrom(src => src.Email)).ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.DetailedDescription, opt => opt.MapFrom(src => src.DetailedDescription))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                 .ForMember(dest => dest.Startingprice, opt => opt.MapFrom(src => src.Startingprice)).ReverseMap();

            CreateMap<Buyer, BuyerDto>().ReverseMap();
            CreateMap<Product, ListProductBidDto>().ForMember(dset => dset.PlacedBids, opt => opt.MapFrom(src => src.Buyers)).ReverseMap();
            CreateMap<Buyer, BuyerDto>().ReverseMap();
            CreateMap<BuyerProductBidDto, Buyer>().ForMember(dset => dset.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                 .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                 .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                 .ForMember(dest => dest.Pin, opt => opt.MapFrom(src => src.Pin))
                 .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                 /*   .ForMember(dest => dest.product.Id, opt => opt.MapFrom(src => src.ProductId))
                    .ForMember(dest => dest.product.ProductName, opt => opt.MapFrom(src => src.ProductName))
                    .ForMember(dest => dest.product.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                    .ForMember(dest => dest.product.DetailedDescription, opt => opt.MapFrom(src => src.DetailedDescription))
                    .ForMember(dest => dest.product.Category, opt => opt.MapFrom(src => src.Category))
                    .ForMember(dest => dest.product.Startingprice, opt => opt.MapFrom(src => src.Startingprice))*/.ReverseMap();
        } 
        
    }
}
