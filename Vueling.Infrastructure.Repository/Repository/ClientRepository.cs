using AutoMapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vueling.Common.Layer;
using Vueling.Domain.Entities;
using Vueling.Infrastructure.Repository.Contracts;
using Vueling.Infrastructure.Repository.DataModel;

namespace Vueling.Infrastructure.Repository.Repository {
    public class ClientRepository : IRepository<ClientEntity>, IClientRepository<ClientEntity> {

        public ClientRepository() {
            #region Init Log
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConfigurationManager.AppSettings["ErrorLog"].ToString(), fileSizeLimitBytes: 1000)
                .CreateLogger();
            #endregion
        }        

        public List<ClientEntity> AddList(List<ClientEntity> listClientEntities) {
            List<Client> listClients = null;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ClientEntity, Client>().ReverseMap());
            IMapper iMapper = config.CreateMapper();

            listClients = iMapper.Map<List<ClientEntity>, List<Client>>(listClientEntities);
            using (var db = new VuelingEntities()) {

                if (HasTheDbBeenModified(listClients, db)) {

                    DeleteAllTablesInOrder(db);

                    using (var dbTransaction = db.Database.BeginTransaction()) {
                        try {
                            db.Client.AddRange(listClients);

                            db.SaveChanges();
                            dbTransaction.Commit();
                        }
                        #region Exceptions With Log
                    catch (DbUpdateConcurrencyException e) {
                            Log.Error(Resource_Infrastructure_Repository.ConcurrencyError
                                + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.StackTrace);

                            dbTransaction.Rollback();

                            throw new VuelingException(Resource_Infrastructure_Repository.ConcurrencyError, e);

                        } catch (DbUpdateException e) {
                            Log.Error(Resource_Infrastructure_Repository.DbUpdateError
                                + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.StackTrace);

                            dbTransaction.Rollback();

                            throw new VuelingException(Resource_Infrastructure_Repository.DbUpdateError, e);

                        } catch (DbEntityValidationException e) {
                            Log.Error(Resource_Infrastructure_Repository.DbEntityValidationError
                                + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.StackTrace);

                            dbTransaction.Rollback();

                            throw new VuelingException(Resource_Infrastructure_Repository.DbEntityValidationError, e);

                        } catch (NotSupportedException e) {
                            Log.Error(Resource_Infrastructure_Repository.NotSuportedError
                                + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.StackTrace);

                            dbTransaction.Rollback();

                            throw new VuelingException(Resource_Infrastructure_Repository.NotSuportedError, e);

                        } catch (ObjectDisposedException e) {
                            Log.Error(Resource_Infrastructure_Repository.ObjectDisposedError
                                + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.StackTrace);

                            dbTransaction.Rollback();

                            throw new VuelingException(Resource_Infrastructure_Repository.ObjectDisposedError, e);

                        } catch (InvalidOperationException e) {
                            Log.Error(Resource_Infrastructure_Repository.InvalidOperationError
                                + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                                + e.StackTrace);

                            dbTransaction.Rollback();

                            throw new VuelingException(Resource_Infrastructure_Repository.InvalidOperationError, e);
                            #endregion
                        }
                    }
                }
            }
            List<ClientEntity> listClientEntitiesAdded = iMapper.Map<List<Client>, List<ClientEntity>>(listClients);
            
            return listClientEntitiesAdded;
        }

        public bool HasTheDbBeenModified(List<Client> newListClients, VuelingEntities db) {
            List<Client> currentListClients = db.Client.OrderBy(x => x.Name).ToList();

            if (newListClients.Count() == currentListClients.Count()) {

                newListClients = newListClients.OrderBy(x => x.Name).ToList();

                for (int i = 0; i < currentListClients.Count(); i++) {

                    if (!currentListClients[i].Id.Equals(newListClients[i].Id)) {
                        return true;
                    }
                }
            } else return true;

            return false;
        }


        public void DeleteAllTablesInOrder(VuelingEntities db) {
            using (var dbTransaction = db.Database.BeginTransaction()) {
                try {
                    DeleteAllRowsOfPolicyTable(db);
                    DeleteAllRowsOfClientTable(db);

                    db.SaveChanges();
                    dbTransaction.Commit();
                }
                #region Exceptions With Log
                catch (DbUpdateConcurrencyException e) {
                    Log.Error(Resource_Infrastructure_Repository.ConcurrencyError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    dbTransaction.Rollback();

                    throw new VuelingException(Resource_Infrastructure_Repository.ConcurrencyError, e);

                } catch (DbUpdateException e) {
                    Log.Error(Resource_Infrastructure_Repository.DbUpdateError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    dbTransaction.Rollback();

                    throw new VuelingException(Resource_Infrastructure_Repository.DbUpdateError, e);

                } catch (DbEntityValidationException e) {
                    Log.Error(Resource_Infrastructure_Repository.DbEntityValidationError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    dbTransaction.Rollback();

                    throw new VuelingException(Resource_Infrastructure_Repository.DbEntityValidationError, e);

                } catch (NotSupportedException e) {
                    Log.Error(Resource_Infrastructure_Repository.NotSuportedError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    dbTransaction.Rollback();

                    throw new VuelingException(Resource_Infrastructure_Repository.NotSuportedError, e);

                } catch (ObjectDisposedException e) {
                    Log.Error(Resource_Infrastructure_Repository.ObjectDisposedError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    dbTransaction.Rollback();

                    throw new VuelingException(Resource_Infrastructure_Repository.ObjectDisposedError, e);

                } catch (InvalidOperationException e) {
                    Log.Error(Resource_Infrastructure_Repository.InvalidOperationError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    dbTransaction.Rollback();

                    throw new VuelingException(Resource_Infrastructure_Repository.InvalidOperationError, e);
                    #endregion
                }
            }
        }


        public void DeleteAllRowsOfClientTable(VuelingEntities db) {
            db.Database.ExecuteSqlCommand(Resource_Infrastructure_Repository.QueryToDeleteAllRowsInClientTable);
        }

        public void DeleteAllRowsOfPolicyTable(VuelingEntities db) {
            db.Database.ExecuteSqlCommand(Resource_Infrastructure_Repository.QueryToDeleteAllRowsInPolicyTable);
        }


        public ClientEntity Add(ClientEntity model) {

            using (var db = new VuelingEntities()) {
                using (var dbTransaction = db.Database.BeginTransaction()) {
                    try {
                        Client client = null;

                        var config = new MapperConfiguration(cfg => cfg.CreateMap<ClientEntity, Client>().ReverseMap());

                        IMapper iMapper = config.CreateMapper();

                        client = iMapper.Map<ClientEntity, Client>(model);

                        Client clientAdded = db.Client.Add(client);
                        db.SaveChanges();
                        dbTransaction.Commit();

                        ClientEntity clientEntityAdded = iMapper.Map<Client, ClientEntity>(clientAdded);

                        return clientEntityAdded;
                    }
                    #region Exceptions With Log
                    catch (DbUpdateConcurrencyException e) {
                        Log.Error(Resource_Infrastructure_Repository.ConcurrencyError
                            + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.StackTrace);

                        dbTransaction.Rollback();

                        throw new VuelingException(Resource_Infrastructure_Repository.ConcurrencyError, e);

                    } catch (DbUpdateException e) {
                        Log.Error(Resource_Infrastructure_Repository.DbUpdateError
                            + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.StackTrace);

                        dbTransaction.Rollback();

                        throw new VuelingException(Resource_Infrastructure_Repository.DbUpdateError, e);

                    } catch (DbEntityValidationException e) {
                        Log.Error(Resource_Infrastructure_Repository.DbEntityValidationError
                            + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.StackTrace);

                        dbTransaction.Rollback();

                        throw new VuelingException(Resource_Infrastructure_Repository.DbEntityValidationError, e);

                    } catch (NotSupportedException e) {
                        Log.Error(Resource_Infrastructure_Repository.NotSuportedError
                            + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.StackTrace);

                        dbTransaction.Rollback();

                        throw new VuelingException(Resource_Infrastructure_Repository.NotSuportedError, e);

                    } catch (ObjectDisposedException e) {
                        Log.Error(Resource_Infrastructure_Repository.ObjectDisposedError
                            + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.StackTrace);

                        dbTransaction.Rollback();

                        throw new VuelingException(Resource_Infrastructure_Repository.ObjectDisposedError, e);

                    } catch (InvalidOperationException e) {
                        Log.Error(Resource_Infrastructure_Repository.InvalidOperationError
                            + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                            + e.StackTrace);

                        dbTransaction.Rollback();

                        throw new VuelingException(Resource_Infrastructure_Repository.InvalidOperationError, e);
                        #endregion
                    }
                }
            }
        }


        public List<ClientEntity> GetAll() {
            List<ClientEntity> ListClientEntities;
            List<Client> ListClients;

            using (var db = new VuelingEntities()) {
                try {
                    ListClients = db.Client.ToList();

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientEntity>());

                    IMapper iMapper = config.CreateMapper();

                    ListClientEntities = iMapper.Map<List<Client>, List<ClientEntity>>(ListClients);

                    return ListClientEntities;
                }
                #region Exceptions With Log
                        catch (DbUpdateConcurrencyException e) {
                    Log.Error(Resource_Infrastructure_Repository.ConcurrencyError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.ConcurrencyError, e);

                } catch (DbUpdateException e) {
                    Log.Error(Resource_Infrastructure_Repository.DbUpdateError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.DbUpdateError, e);

                } catch (DbEntityValidationException e) {
                    Log.Error(Resource_Infrastructure_Repository.DbEntityValidationError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.DbEntityValidationError, e);

                } catch (NotSupportedException e) {
                    Log.Error(Resource_Infrastructure_Repository.NotSuportedError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.NotSuportedError, e);

                } catch (ObjectDisposedException e) {
                    Log.Error(Resource_Infrastructure_Repository.ObjectDisposedError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.ObjectDisposedError, e);

                } catch (InvalidOperationException e) {
                    Log.Error(Resource_Infrastructure_Repository.InvalidOperationError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.InvalidOperationError, e);
                    #endregion
                }
                
            }
        }


        public ClientEntity GetById(string id) {
            ClientEntity clientEntity;
            Client client;

            using (var db = new VuelingEntities()) {
                try {
                    client = db.Client.FirstOrDefault(c => c.Id.Equals(id));

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientEntity>());

                    IMapper iMapper = config.CreateMapper();

                    clientEntity = iMapper.Map<Client, ClientEntity>(client);

                    return clientEntity;
                }
                #region Exceptions With Log
                        catch (DbUpdateConcurrencyException e) {
                    Log.Error(Resource_Infrastructure_Repository.ConcurrencyError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.ConcurrencyError, e);

                } catch (DbUpdateException e) {
                    Log.Error(Resource_Infrastructure_Repository.DbUpdateError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.DbUpdateError, e);

                } catch (DbEntityValidationException e) {
                    Log.Error(Resource_Infrastructure_Repository.DbEntityValidationError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                   throw new VuelingException(Resource_Infrastructure_Repository.DbEntityValidationError, e);

                } catch (NotSupportedException e) {
                    Log.Error(Resource_Infrastructure_Repository.NotSuportedError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.NotSuportedError, e);

                } catch (ObjectDisposedException e) {
                    Log.Error(Resource_Infrastructure_Repository.ObjectDisposedError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.ObjectDisposedError, e);

                } catch (InvalidOperationException e) {
                    Log.Error(Resource_Infrastructure_Repository.InvalidOperationError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.InvalidOperationError, e);
                    #endregion
                }
            }
        }

        public ClientEntity GetByName(string name) {
            ClientEntity clientEntity;
            Client client;

            using (var db = new VuelingEntities()) {
                try {
                    client = db.Client.FirstOrDefault(c => c.Name.Equals(name));

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientEntity>());

                    IMapper iMapper = config.CreateMapper();

                    clientEntity = iMapper.Map<Client, ClientEntity>(client);

                    return clientEntity;
                }
                #region Exceptions With Log
                    catch (DbUpdateConcurrencyException e) {
                    Log.Error(Resource_Infrastructure_Repository.ConcurrencyError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.ConcurrencyError, e);

                } catch (DbUpdateException e) {
                    Log.Error(Resource_Infrastructure_Repository.DbUpdateError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);
                    throw new VuelingException(Resource_Infrastructure_Repository.DbUpdateError, e);

                } catch (DbEntityValidationException e) {
                    Log.Error(Resource_Infrastructure_Repository.DbEntityValidationError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.DbEntityValidationError, e);

                } catch (NotSupportedException e) {
                    Log.Error(Resource_Infrastructure_Repository.NotSuportedError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.NotSuportedError, e);

                } catch (ObjectDisposedException e) {
                    Log.Error(Resource_Infrastructure_Repository.ObjectDisposedError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.ObjectDisposedError, e);

                } catch (InvalidOperationException e) {
                    Log.Error(Resource_Infrastructure_Repository.InvalidOperationError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.InvalidOperationError, e);
                    #endregion
                }
            }
        }


        public ClientEntity GetUserByPolicyId(string idPolicy) {
            ClientEntity clientEntity;
            Client client;

            using (var db = new VuelingEntities()) {
                try {
                    client = db.Policy
                        .Select(p => p.Client)
                        .FirstOrDefault(p => p.Id.Equals(idPolicy));

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientEntity>());

                    IMapper iMapper = config.CreateMapper();

                    clientEntity = iMapper.Map<Client, ClientEntity>(client);

                    return clientEntity;
                }
                #region Exceptions With Log
                    catch (DbUpdateConcurrencyException e) {
                    Log.Error(Resource_Infrastructure_Repository.ConcurrencyError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.ConcurrencyError, e);

                } catch (DbUpdateException e) {
                    Log.Error(Resource_Infrastructure_Repository.DbUpdateError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.DbUpdateError, e);

                } catch (DbEntityValidationException e) {
                    Log.Error(Resource_Infrastructure_Repository.DbEntityValidationError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.DbEntityValidationError, e);

                } catch (NotSupportedException e) {
                    Log.Error(Resource_Infrastructure_Repository.NotSuportedError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.NotSuportedError, e);

                } catch (ObjectDisposedException e) {
                    Log.Error(Resource_Infrastructure_Repository.ObjectDisposedError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.ObjectDisposedError, e);

                } catch (InvalidOperationException e) {
                    Log.Error(Resource_Infrastructure_Repository.InvalidOperationError
                        + e.Message + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.Data + Resource_Infrastructure_Repository.ErrorLogSeparation
                        + e.StackTrace);

                    throw new VuelingException(Resource_Infrastructure_Repository.InvalidOperationError, e);
                    #endregion
                }
                
            }
        }
    }
}
