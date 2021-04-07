using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Controllers;
using WebApplication1.Data;
using WebApplication1.Models;
using Xunit;

namespace TestProject1 {
    public class UnitTest1 {
        private DbContextOptions<ProjContext> options;

        private void InitializeDataBase() {
            options = new DbContextOptionsBuilder<ProjContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using (var context = new ProjContext(options)) {
                context.Atividade.Add(new Atividade { 
                    Descricao = "ativ 1", 
                    DataInicio = DateTime.ParseExact("04/01/2021", "d", CultureInfo.InvariantCulture), 
                    DataFim = DateTime.ParseExact("05/22/2021", "d", CultureInfo.InvariantCulture),
                    Responsavel = new Responsavel {Id = 1, Nome = "Leo"}
                });
                context.SaveChanges();

                context.Atividade.Add(new Atividade { 
                    Descricao = "ativ 2", 
                    DataInicio = DateTime.ParseExact("04/01/2021", "d", CultureInfo.InvariantCulture), 
                    DataFim = DateTime.ParseExact("05/22/2021", "d", CultureInfo.InvariantCulture),
                    Responsavel = context.Responsavel.FirstOrDefault(a => a.Id == 1)
                });                
                context.SaveChanges();
            }
        }
        [Fact]
        public void GetAll() {
            InitializeDataBase();

            using (var context = new ProjContext(options)) {
                AtividadesController atividadesController = new AtividadesController(context);
                IEnumerable<Atividade> atividades = atividadesController.GetAtividade().Result.Value;

                Assert.Equal(2, atividades.Count());
            }
        }

        [Fact]
        public void GetbyId() {
            InitializeDataBase();

            using (var context = new ProjContext(options)) {
                int atividadeId = 2;
                AtividadesController atividadesController = new AtividadesController(context);
                Atividade atividade = atividadesController.GetAtividade(atividadeId).Result.Value;
                Assert.Equal(atividadeId, atividade.Id);
            }
        }

        [Fact]
        public void Create() {
            InitializeDataBase();

            var atividade = new Atividade {
                Id = 10,
                Descricao = "ativ 5",
                DataInicio = DateTime.ParseExact("04/01/2021", "d", CultureInfo.InvariantCulture),
                DataFim = DateTime.ParseExact("05/22/2021", "d", CultureInfo.InvariantCulture),
                Responsavel = new Responsavel { Id = 5, Nome = "Marcio" }
            };
            using (var context = new ProjContext(options)) {
                AtividadesController atividadesController = new AtividadesController(context);
                Atividade atividade2 = atividadesController.PostAtividade(atividade).Result.Value;
                Assert.Equal(10, atividade2.Id);
            }
        }

        [Fact]
        public void Update() {
            InitializeDataBase();

            Atividade atividade = new Atividade {
                Id = 1,
                Descricao = "ativ 90",
                DataInicio = DateTime.ParseExact("04/01/2021", "d", CultureInfo.InvariantCulture),
                DataFim = DateTime.ParseExact("05/22/2021", "d", CultureInfo.InvariantCulture),
                Responsavel = new Responsavel { Id = 2, Nome = "Kawan" }
            };

            using (var context = new ProjContext(options)) {
                AtividadesController atividadesController = new AtividadesController(context);
                Atividade atividade2 = atividadesController.PutAtividade(1, atividade).Result.Value;
                Assert.Equal("ativ 90", atividade2.Descricao);
            }
        }

        [Fact]
        public void Delete() {
            InitializeDataBase();

            using (var context = new ProjContext(options)) {
                AtividadesController atividadesController = new AtividadesController(context);
                Atividade atividade = atividadesController.DeleteAtividade(1).Result.Value;
                Assert.Null(atividade);
            }
        }
    }
}


