using System;
using System.Collections.Generic;
using System.Text;
using Factura.Domain;
using Factura.Repository;

namespace Factura.Service.Implementation
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public bool DeleteById(int id)
        {
            return usuarioRepository.DeleteById(id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return usuarioRepository.GetAll();
        }

        public Usuario GetById(int id)
        {
            return usuarioRepository.GetById(id);
        }

        public Usuario GetByRUC(long RUC)
        {
            return usuarioRepository.GetByRUC(RUC);
        }

        public Usuario Login(string email, string contraseña)
        {
            return usuarioRepository.Login(email, contraseña);
        }

        public bool Save(Usuario entity)
        {
            return usuarioRepository.Save(entity);
        }

        public bool Update(Usuario entity)
        {
            return usuarioRepository.Update(entity);
        }
    }
}