﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using uff.Domain.Commands.User;
using uff.Domain.Enum;

namespace uff.Domain.Entity
{
    public class User : To
    {
        private User()
        {
        }

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string StateId { get; private set; }
        public string Cpf { get; set; }
        public StatusEnum Status { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public int? StoreId { get; private set; }
        public ProfileEnum Profile { get; private set; }
        public ICollection<Store> Stores { get; set; } = new List<Store>();

        public User(UserCreateCommand user)
        {
            Name = user.Name;
            Email = user.Email;
            LastName = user.LastName;
            Phone = user.Phone;
            Address = user.Address;
            Password = user.Password;
            StateId = user.State;
            Number = user.Number;
            Status = StatusEnum.Enabled;
            City = user.City;
            RegisteringDate = DateTime.UtcNow;
            LastUpdate = DateTime.UtcNow;
            Profile = ProfileEnum.User;
        }

        public void UpdateAllUserInfo(UserEditCommand user)
        {
            Name = !string.IsNullOrWhiteSpace(user.Name) ? user.Name : this.Name;
            LastName = !string.IsNullOrWhiteSpace(user.LastName) ? user.LastName : this.LastName;            
            Phone = !string.IsNullOrWhiteSpace(user.Phone) ? user.Phone : this.Phone;
            Address = !string.IsNullOrWhiteSpace(user.Street) ? user.Street : this.Address;
            Number = !string.IsNullOrWhiteSpace(user.Number) ? user.Number : this.Number;
            City = !string.IsNullOrWhiteSpace(user.City) ? user.City : this.City;
            Status = user.Status;
            LastUpdate = DateTime.UtcNow;

            UpdateCpf(user.Cpf);
            CheckProfileChange(user.Profile);
        }

        public void UpdatePassWord(string passWord)
            => Password = passWord;

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName)
                || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Address)
                || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Number)
                || string.IsNullOrEmpty(City));
        }

        public void Disable()
            => Status = StatusEnum.Disabled;

        public void Enable()
            => Status = StatusEnum.Enabled;

        private void UpdateCpf(string cpfcnpj)
        {
            if (!string.IsNullOrWhiteSpace(cpfcnpj) && (cpfcnpj.Length == 11))
                this.Cpf = cpfcnpj;
        }            

        private void CheckProfileChange(ProfileEnum newProfile)
        {
            if (newProfile != ProfileEnum.Admin)
                this.Profile = newProfile;
        }
    }
}
