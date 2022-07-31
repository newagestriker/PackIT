using PackIT.Application.Exceptions;
using PackIT.Application.Services;
using PackIT.Domain.Factory;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Application.Commands.Handlers
{
    public class CreatepackingListWithItemsHandler : ICommandHandler<CreatepackingListWithItems>
    {
        private readonly IPackingListRepository _repository;
        private readonly IPackingListFactory _factory;
        private readonly IPackingListReadService _readService;
        private readonly IWeatherService _weatherService;

        public CreatepackingListWithItemsHandler(IPackingListRepository repository, IPackingListFactory factory, IPackingListReadService readService, IWeatherService weatherService = null)
        {
            _repository = repository;
            _factory = factory;
            _readService = readService;
            _weatherService = weatherService;
        }

        public async Task HandleAsync(CreatepackingListWithItems command)
        {
            var (id,name,days,gender,localizationWriteModel) = command;
            if(await _readService.ExistsByName(name))
            {
                throw new PackingListAlreadyExistsException(name);
            }

            var localization = new Localization(localizationWriteModel.City,localizationWriteModel.Country);
            var weather = await _weatherService.GetWeatherAsync(localization);

            if(weather is null)
            {
                throw new MissingLocalizationWeatherException(localization);
            }

            var packingList = _factory.CreateWithDefaultItems(id,name,days,gender,weather.Temperature,localization);

            await _repository.AddAsync(packingList);
           
        }
    }
}
