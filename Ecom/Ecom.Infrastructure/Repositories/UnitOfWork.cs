using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IImageManagementService _imageManagementService;

    public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService)
    {
        _context = context;

        _mapper = mapper;

        _imageManagementService = imageManagementService;


        CategoryRepository = new CategoryRepository(_context);

        ProductRepository = new ProductRepository(_context, mapper, _imageManagementService);

        PhotoRepository = new PhotoRepository(_context);
     
    }




    public ICategoryRepository CategoryRepository { get; }

    public IProductRepository ProductRepository { get; }

    public IPhotoRepository PhotoRepository { get; }

}
