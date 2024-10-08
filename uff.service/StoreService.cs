﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uff.Domain;
using uff.Domain.Commands;
using uff.Domain.Commands.Store;
using uff.Domain.Dto;
using uff.Domain.Entity;
using uff.Service.Properties;

namespace uff.Service
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository repository, IMapper mapper, IUserRepository userRepository)
        {
            _storeRepository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<CommandResult> GetAllAsync()
        {
            var stores = await _storeRepository.GetAllAsync();

            if (stores is null || stores.Count() == 0)
                return new CommandResult(false, stores);

            return new CommandResult(true, _mapper.Map<List<StoreDto>>(stores));
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);

            if (store is null)
                return new CommandResult(false, store);            

            return new CommandResult(true, _mapper.Map<StoreDto>(store));
        }      

        public async Task<CommandResult> CreateAsync(StoreCreateCommand command)
        {
            try
            {             
                var store = new Store(command);

                if (!store.IsValid())
                    return new CommandResult(false, Resources.MissingInfo);

                var owner = await _userRepository.GetByIdAsync(command.OwnerId);

                if (owner is null)
                    return new CommandResult(false, Resources.OwnerNotFound);

                store.SetOwner(owner);

                await _storeRepository.AddAsync(store);
                await _storeRepository.SaveChangesAsync();

                return new CommandResult(true, store);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        public async Task<CommandResult> UpdateAsync(StoreEditCommand command)
        {
            try
            {
                var store = await _storeRepository.GetByIdAsync(command.Id);

                if (store is null)
                    return new CommandResult(false, Resources.NotFound);

                store.UpdateAllUserInfo(command);

                _storeRepository.Update(store);
                await _storeRepository.SaveChangesAsync();

                return new CommandResult(true, _mapper.Map<UserDto>(store));
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        public async Task<CommandResult> DeleteAsync(int id)
        {
            try
            {
                var store = await _storeRepository.GetByIdAsync(id);
                if (store is not null)
                {
                    store.Disable();
                    _storeRepository.Update(store);
                    await _storeRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }

            return new CommandResult(true, null);
        }
    }
}