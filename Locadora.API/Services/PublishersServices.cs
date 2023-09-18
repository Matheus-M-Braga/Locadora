using Locadora.API.Data;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.API.Services {
    public class PublishersServices {
        public readonly IRepository _repo;
        public PublishersServices(IRepository repo) {
            _repo = repo;
        }
        public async Task<bool> VerifyName(string name) {
            var existingPublisher = await _repo.GetPublisherByName(name);
            if(existingPublisher != null){
                return true;
            }
            return false;
        }
    }
}
