using Locadora.API.Data;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.API.Services {
    public class ManagePublishers {
        public readonly IRepository _repo;
        public ManagePublishers(IRepository repo) {
            _repo = repo;
        }
        public Publishers VerifyName(string name) {
            var existingPublisher = _repo.GetPublisherByName(name);
            return existingPublisher; 
        }
    }
}
