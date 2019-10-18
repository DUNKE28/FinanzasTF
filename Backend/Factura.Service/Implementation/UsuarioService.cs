using System;
using System.Collections.Generic;
using System.Text;
using Factura.Domain;
using Factura.Repository;

namespace Factura.Service.Implementation
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository userRepository;

        public UsuarioService(IUsuarioRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public Usuario GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario GetByRUC(long RUC)
        {
            throw new NotImplementedException();
        }

        public Usuario Login(string email, string contraseña)
        {
            throw new NotImplementedException();
        }

        public bool Save(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Usuario entity)
        {
            throw new NotImplementedException();
        }
    }
}