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
    public class PolicyRepository : IRepository<PolicyEntity>, IPolicyRepository<PolicyEntity> {

        public PolicyRepository() {
            #region Init Log
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConfigurationManager.AppSettings["ErrorLog"].ToString(), fileSizeLimitBytes: 1000)
                .CreateLogger();
            #endregion
        }


        public PolicyEntity Add(PolicyEntity model) {
            using (var db = new VuelingEntities()) {
                using (var dbTransaction = db.Database.BeginTransaction()) {
                    try {
                        Policy policy = null;

                        var config = new MapperConfiguration(cfg => cfg.CreateMap<PolicyEntity, Policy>()
                           .ReverseMap());

                        IMapper iMapper = config.CreateMapper();

                        policy = iMapper.Map<PolicyEntity, Policy>(model);

                        Policy policyAdded = db.Policy.Add(policy);
                        db.SaveChanges();
                        dbTransaction.Commit();

                        PolicyEntity policyEntityAdded = iMapper.Map<Policy, PolicyEntity>(policyAdded);

                        return policyEntityAdded;

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

        public List<PolicyEntity> AddList(List<PolicyEntity> listPolicyEntities) {
            using (var db = new VuelingEntities()) {
                using (var dbTransaction = db.Database.BeginTransaction()) {
                    try {
                        List<Policy> listPolicies = null;

                        var config = new MapperConfiguration(cfg => cfg.CreateMap<PolicyEntity, Policy>().ReverseMap());
                        IMapper iMapper = config.CreateMapper();

                        listPolicies = iMapper.Map<List<PolicyEntity>, List<Policy>>(listPolicyEntities);

                        if (HasTheDbBeenModified(listPolicies)) {

                            DeleteAllRowsOfPolicyTable();

                            foreach (var policy in listPolicies) {
                                db.Policy.Add(policy);
                            }
                            db.SaveChanges();
                            dbTransaction.Commit();
                        }
                        List<PolicyEntity> listPolicyEntitiesAdded = iMapper.Map<List<Policy>, List<PolicyEntity>>(listPolicies);

                        return listPolicyEntitiesAdded;
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

        public List<PolicyEntity> GetAll() {
            List<PolicyEntity> ListPolicyEntities;
            List<Policy> ListPolicies;

            using (var db = new VuelingEntities()) {
                using (var dbTransaction = db.Database.BeginTransaction()) {
                    try {
                        ListPolicies = db.Policy.ToList();

                        var config = new MapperConfiguration(cfg => cfg.CreateMap<Policy, PolicyEntity>());

                        IMapper iMapper = config.CreateMapper();

                        ListPolicyEntities = iMapper.Map<List<Policy>, List<PolicyEntity>>(ListPolicies);

                        return ListPolicyEntities;
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


        public List<PolicyEntity> GetPoliciesByUserName(string username) {
            List<PolicyEntity> listPolicyEntities;
            List<Policy> listPolicies;

            using (var db = new VuelingEntities()) {
                using (var dbTransaction = db.Database.BeginTransaction()) {
                    try {
                        listPolicies = db.Policy.Where(p => p.ClientId.Equals(username)).ToList();

                        var config = new MapperConfiguration(cfg => cfg.CreateMap<Policy, PolicyEntity>());

                        IMapper iMapper = config.CreateMapper();

                        listPolicyEntities = iMapper.Map<List<Policy>, List<PolicyEntity>>(listPolicies);

                        return listPolicyEntities;
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

        public bool HasTheDbBeenModified(List<Policy> newListPolicies) {
            using (var db = new VuelingEntities()) {
                try {
                    List<Policy> currentListPolicies = db.Policy.OrderBy(x => x.Id).ToList();

                    if (newListPolicies.Count() == currentListPolicies.Count()) {

                        newListPolicies = newListPolicies.OrderBy(x => x.Id).ToList();

                        for (int i = 0; i < currentListPolicies.Count(); i++) {

                            if (!currentListPolicies[i].Id.Equals(newListPolicies[i].Id)) {
                                return true;
                            }
                        }
                    } else return true;

                    return false;
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

        public void DeleteAllRowsOfPolicyTable() {
            using (var db = new VuelingEntities()) {
                using (var dbTransaction = db.Database.BeginTransaction()) {
                    try {
                        db.Database.ExecuteSqlCommand(Resource_Infrastructure_Repository.QueryToDeleteAllRowsInPolicyTable);

                        db.SaveChanges();
                        dbTransaction.Commit();
                    } catch (Exception e) {
                        dbTransaction.Rollback();

                        throw new VuelingException(Resource_Infrastructure_Repository.CantDeleteMsg, e);
                    }
                }
            }
        }
    }
}
