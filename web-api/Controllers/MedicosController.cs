using System;
using System.Web.Http;

namespace web_api.Controllers
{
    public class MedicosController : ApiController
    {

        private readonly Repositories.IRepository<Models.Medico> repository;

        public MedicosController()
        {
            try
            {
                repository = new Repositories.Database.SQLServer.ADO.Medico(Configurations.SQLServer.getConnectionString());
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());
            }
        }

        // GET: api/Medicos
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(repository.get());
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());

                return InternalServerError();
            }
        }

        // GET: api/Medicos/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Models.Medico medico = repository.getById(id);

                if (medico == null)
                    return NotFound();

                return Ok(medico);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());

                return InternalServerError();
            }
        }

        // POST: api/Medicos
        public IHttpActionResult Post(Models.Medico medico)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                repository.add(medico);

                return Ok(medico);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());

                return InternalServerError();
            }
        }

        // PUT: api/Medicos/5
        public IHttpActionResult Put(int id, Models.Medico medico)
        {
            try
            {
                if (id != medico.Codigo)
                    ModelState.AddModelError("Codigo", "Código enviado no parâmetro é diferente do código do médico");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                

                int linhasAfetadas = repository.update(id, medico);

                if (linhasAfetadas == 0)
                    return NotFound();

                return Ok(medico);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());

                return InternalServerError();
            }
        }

        // DELETE: api/Medicos/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                int linhasAfetadas = repository.delete(id);

                if (linhasAfetadas == 0)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Log.getFullPath());

                return InternalServerError();
            }
        }
    }
}
