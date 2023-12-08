using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OpenDotaDotNet.Models.Metadata;
using PudgeSMomom.Controllers;
using PudgeSMomom.Models.AdvertModels;
using PudgeSMomom.Models;
using PudgeSMomom.Services.Cloudinary;
using PudgeSMomom.Services.Repository.AdvertRepository;
using PudgeSMomom.Services.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PudgeSMomom.ViewModels.AdvertVMs;
using System.Security.Claims;
using Xunit;

namespace PudgeSMomom.Tests.Controllers
{
    public class AdvertControllerTests
    {
        private AdvertController _advertController;
        IAdvertRepository _advertRepository;
        IPhotoService _photoService;
        IUserRepository _userRepository;
        private readonly UserManager<Models.User> _userManager;
        public AdvertControllerTests()
        {
            _advertRepository = A.Fake<IAdvertRepository>();
            _photoService = A.Fake<IPhotoService>();
            _userManager = A.Fake<UserManager<Models.User>>();
            _userRepository = A.Fake<IUserRepository>();

            _advertController = new AdvertController(_advertRepository, _photoService, _userRepository, _userManager);


        }

        [Fact]
        public void AdvertController_Index_ReturnsSuccess()
        {
            #region Arrange
            var adverts = A.Fake<IEnumerable<Advert>>();
            A.CallTo(() => _advertRepository.GetAdverts()).Returns(adverts);
            #endregion
            
            #region Act
            var result = _advertController.Index();
            #endregion
            
            #region Assert
            result.Should().BeOfType<Task<IActionResult>>();
            #endregion
        }

        [Fact]
        public void AdvertController_Deatail_ReturnsSuccess()
        {
            #region Arrange
            int id = 1;
            var advert = A.Fake<Advert>();
            A.CallTo(() => _advertRepository.GetAdvertByIdAsync(id));
            #endregion

            #region Act
            var result = _advertController.Detail(id);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<IActionResult>>();
            #endregion
        }

     
    }
}
