using AutoMapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vueling.Application.Dto;
using Vueling.Application.Services.Contracts;
using Vueling.Common.Layer;
using Vueling.Domain.Entities;
using Vueling.Infrastructure.Repository.Contracts;
using Vueling.Infrastructure.Repository.Repository;

namespace Vueling.Application.Services.Service {
    public class PolicyService : IService<PolicyDto>, IPolicyService<PolicyDto> {
        private readonly PolicyRepository policyRepository;

        public PolicyService() : this(new PolicyRepository()) {
            #region Init Log
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConfigurationManager.AppSettings["ErrorLog"].ToString(), fileSizeLimitBytes: 1000)
                .CreateLogger();
            #endregion
        }

        public PolicyService(PolicyRepository policyRepository) {
            this.policyRepository = policyRepository;
            #region Init Log
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConfigurationManager.AppSettings["ErrorLog"].ToString(), fileSizeLimitBytes: 1000)
                .CreateLogger();
            #endregion
        }


        public PolicyDto Add(PolicyDto policyDto) {
            PolicyEntity policyEntity = null;
            try {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PolicyDto, PolicyEntity>().ReverseMap());

                IMapper iMapper = config.CreateMapper();

                policyEntity = iMapper.Map<PolicyDto, PolicyEntity>(policyDto);
                PolicyEntity policyEntityAdded = policyRepository.Add(policyEntity);

                return iMapper.Map<PolicyEntity, PolicyDto>(policyEntityAdded);

            }
            #region Exceptions With Log
             catch (NotSupportedException e) {
                Log.Error(Resource_Application_Services.NotSuportedError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.NotSuportedError, e);

            } catch (ObjectDisposedException e) {
                Log.Error(Resource_Application_Services.ObjectDisposedError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.ObjectDisposedError, e);

            } catch (InvalidOperationException e) {
                Log.Error(Resource_Application_Services.InvalidOperationError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.InvalidOperationError, e);
                #endregion
            }
        }

        public List<PolicyDto> AddList(List<PolicyDto> listClientDto) {
            List<PolicyEntity> ListPolicyEntities;

            try {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PolicyEntity, PolicyDto>().ReverseMap());
                IMapper iMapper = config.CreateMapper();

                ListPolicyEntities = iMapper.Map<List<PolicyDto>, List<PolicyEntity>>(listClientDto);

                List<PolicyEntity> ListPolicyEntitiesAdded = policyRepository.AddList(ListPolicyEntities);

                List<PolicyDto> listPolicyDtoAdded = iMapper.Map<List<PolicyEntity>, List<PolicyDto>>(ListPolicyEntitiesAdded);

                return listPolicyDtoAdded;
            }
            #region Exceptions With Log
             catch (NotSupportedException e) {
                Log.Error(Resource_Application_Services.NotSuportedError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.NotSuportedError, e);

            } catch (ObjectDisposedException e) {
                Log.Error(Resource_Application_Services.ObjectDisposedError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.ObjectDisposedError, e);

            } catch (InvalidOperationException e) {
                Log.Error(Resource_Application_Services.InvalidOperationError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.InvalidOperationError, e);
                #endregion
            }
        }

        public List<PolicyDto> Get() {
            List<PolicyEntity> listPolicyEntities;

            try {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PolicyEntity, PolicyDto>().ReverseMap());

                IMapper iMapper = config.CreateMapper();

                listPolicyEntities = policyRepository.GetAll();

                return iMapper.Map<List<PolicyEntity>, List<PolicyDto>>(listPolicyEntities);
            }
            #region Exceptions With Log
             catch (NotSupportedException e) {
                Log.Error(Resource_Application_Services.NotSuportedError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.NotSuportedError, e);

            } catch (ObjectDisposedException e) {
                Log.Error(Resource_Application_Services.ObjectDisposedError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.ObjectDisposedError, e);

            } catch (InvalidOperationException e) {
                Log.Error(Resource_Application_Services.InvalidOperationError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.InvalidOperationError, e);
                #endregion
            }
        }

        public PolicyDto GetById(string id) {
            throw new NotImplementedException();
        }

        public List<PolicyDto> GetPoliciesByUserName(string username) {
            List<PolicyDto> listPolicyDto;
            List<PolicyEntity> listPolicyEntities;

            try {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PolicyEntity, PolicyDto>());

                IMapper iMapper = config.CreateMapper();

                listPolicyEntities = policyRepository.GetPoliciesByUserName(username);

                listPolicyDto = iMapper.Map<List<PolicyEntity>, List<PolicyDto>>(listPolicyEntities);

                return listPolicyDto;
            }
            #region Exceptions With Log
             catch (NotSupportedException e) {
                Log.Error(Resource_Application_Services.NotSuportedError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.NotSuportedError, e);

            } catch (ObjectDisposedException e) {
                Log.Error(Resource_Application_Services.ObjectDisposedError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.ObjectDisposedError, e);

            } catch (InvalidOperationException e) {
                Log.Error(Resource_Application_Services.InvalidOperationError
                    + e.Message + Resource_Application_Services.ErrorLogSeparation
                    + e.Data + Resource_Application_Services.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(Resource_Application_Services.InvalidOperationError, e);
                #endregion
            }
        }
    }
}
