using AutoMapper;
using Company.Hossam.DAL.Models;
using Company.Hossam.PL.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Company.Hossam.DAL.Model;
using Company.Hossam.PL.ViewModels;

namespace Company.Hossam.PL.Mapping
{
	public class SignInProfile : Profile
	{
		public SignInProfile()
		{
			CreateMap<SignInViewModel, ApplicationUser>();
			CreateMap<ApplicationUser, SignInViewModel>();

		}
	}
}
