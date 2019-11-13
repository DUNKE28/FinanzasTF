using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Factura.Domain;
using Factura.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Factura.Repository.Implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ApplicationDbContext context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool DeleteById(int id)
        {
            try
            {
                Usuario usuario = GetById(id);
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Usuario> GetAll()
        {
            var result = new List<Usuario>();
            try
            {
                result = context.Usuarios
                .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public Usuario GetById(int id)
        {
            var result = new Usuario();
            try
            {
                result = context.Usuarios
                    .Single(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public Usuario GetByRUC(long RUC)
        {
            var result = new Usuario();
            try
            {
                result = context.Usuarios
                    .Single(x => x.RUC==RUC);
            }
            catch (System.Exception)
            {
                return null;
            }
            if(result==null)
                return null;
            else
                return result;
        }

        public Usuario Login(string email, string contraseña)
        {
            var result = new Usuario();
            try
            {
                result = context.Usuarios
                    .Single(x => x.Email==email && x.Contraseña==contraseña);
            }
            catch (System.Exception)
            {
                return null;
            }
            if(result==null)
                return null;
            else
                return result;
        }

        public bool Save(Usuario entity)
        {
            try
            { 
                context.Add(entity);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(Usuario entity)
        {
            try
            {
                var originalUser = context.Usuarios
                    .Single(user => user.Id == entity.Id);

                originalUser.Email = entity.Email;
                originalUser.Contraseña = entity.Contraseña;
                originalUser.RazonSocial = entity.RazonSocial;
                originalUser.RUC = entity.RUC;

                context.Usuarios.Update(originalUser);
                context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}