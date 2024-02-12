using AutoMapper;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Dtos;

namespace PuntoVenta.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            // * Usuarios

            CreateMap<AuthRegisterDto, User>();
            CreateMap<User, UserDto>();

            // * Products

            CreateMap<ProductoCrearDto, Product>()
                    .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Product, ProductDto>()
                 .ForMember(dest => dest.Category, opt => opt.MapFrom(MapCategoryDto))
                 .ForMember(dest => dest.UnitMeasurement, opt => opt.MapFrom(MapUnitDto));


            // * Categorias

            CreateMap<CategoryCrearDto, Category>();
            CreateMap<Category, CategoryDto>();

            // * UnitMesearemet

            CreateMap<UnitMeasurement, UnitMeasurementDto>();
            CreateMap<UnitMeasurementCreateDto, UnitMeasurement>();

            // * Sales

            CreateMap<SaleCrearDto, Sale>()
                    .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                    .ForMember(dest => dest.VaucherNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.EmployedId, opt => opt.Ignore())
                    .ForMember(dest => dest.SaleDetails, opt => opt.MapFrom(MapSaleDetails));

            CreateMap<Sale, SaleDto>()
                   .ForMember(dest => dest.Customer, opt => opt.MapFrom(
                        src => new UserSaleDto { Email = src.Customer!.Email, Id = src.Customer.Id })
                   )
                   .ForMember(dest => dest.Employed, opt => opt.MapFrom(
                       src => new UserSaleDto { Email = src.Employed!.Email, Id = src.Employed.Id })
                   )
                   .ForMember(dest => dest.Products, opt => opt.MapFrom(MapSaleDetailDto));

            // * Suppliers

            CreateMap<Supplier, SupplierDto>().ReverseMap();

            // * Purchase

            CreateMap<PurchaseCrearDto, Purchase>()
                    .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                    .ForMember(dest => dest.VaucherNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.PurchaseDetails, opt => opt.MapFrom(MapPurchaseDetails));

            CreateMap<Purchase, PurchaseDto>()
                    .ForMember(dest => dest.Supplier, opt => opt.MapFrom(
                        src => new SupplierDto { Name = src.Supplier!.Name, Adress = src.Supplier.Adress, Phone = src.Supplier.Phone }
                    ))
                    .ForMember(dest => dest.Products, opt => opt.MapFrom(
                        MapPurchaseDetailDto
                    ));

            // * Cash Register

            CreateMap<CashRegister, CashRegisterDto>().ReverseMap();
            CreateMap<CashRegisterCrearDto, CashRegister>()
                    .ForMember(dest => dest.Open, opt => opt.MapFrom(src => false));
        }

        private List<PurchaseDetailDto> MapPurchaseDetailDto(Purchase purchase, PurchaseDto dto)
        {
            var list = new List<PurchaseDetailDto>();

            if (purchase.PurchaseDetails == null) { return list; }

            foreach (var detail in purchase.PurchaseDetails)
            {
                list.Add(new PurchaseDetailDto
                {
                    Quantity = detail.Quantity,
                    SubTotal = detail.SubTotal,
                    ProductName = detail.Product!.Name,
                    ProductUnit = detail.Product.UnitMeasurement!.Symbol
                }); 
            }

            return list;
        }

        private List<PurchaseDetail> MapPurchaseDetails(PurchaseCrearDto dto, Purchase purchase)
        {
            var list = new List<PurchaseDetail>();

            if (dto.Products == null) { return list; }

            foreach (var product in dto.Products)
            {
                list.Add(new PurchaseDetail
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    SubTotal = product.SubTotal,
                });
            }

            return list;
        }

        private List<SaleDetailDto> MapSaleDetailDto(Sale sale, SaleDto dto)
        {
            var list = new List<SaleDetailDto>();

            if (sale.SaleDetails == null) { return list; }

            foreach (var saleItem in sale.SaleDetails)
            {
                list.Add(new SaleDetailDto()
                {
                    Descuento = (decimal)saleItem.Descuento!,
                    Quantity = saleItem.Quantity!,
                    SubTotal = saleItem.SubTotal!,

                    ProductName = saleItem.Product!.Name,
                    ProductSalePrice = saleItem.Product.SalePrice,
                    ProductSize = saleItem.Product.Size,
                    ProductStock = saleItem.Product.Stock,

                    ProductUnitSymbol = saleItem.Product.UnitMeasurement!.Symbol

                }); ; ;
            }

            return list;
        }

        private List<SaleDetail> MapSaleDetails(SaleCrearDto dto, Sale sale)
        {
            var list = new List<SaleDetail>();

            if (dto.SaleDetails == null) { return list; }

            foreach (var detail in dto.SaleDetails)
            {
                list.Add(new SaleDetail()
                {
                    Quantity = detail.Quantity,
                    ProductId = detail.ProductId,
                    UnitPrice = detail.UnitPrice,
                    Descuento = detail.Discount,
                });
            }
            return list;
        }

        private UnitMeasurementDto MapUnitDto(Product product, ProductDto dto)
        {
            var result = new UnitMeasurementDto();

            if (product.UnitMeasurement == null) { return result; }

            result.Name = product.UnitMeasurement.Name;
            result.Symbol = product.UnitMeasurement.Symbol;

            return result;
        }

        private CategoryDto MapCategoryDto(Product product, ProductDto dto)
        {
            var result = new CategoryDto();

            if (product.Category == null) { return result; }

            result.Name = product.Category.Name;
            result.Description = product.Category.Description;
            result.Id = product.Category.Id;

            return result;
        }
    }
}
