
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryFiles;

namespace UserInterface.ViewComponents
{
    public class ContactsViewComponent : ViewComponent
    {
        private IAddressBookActions actions;
        public ContactsViewComponent(IAddressBookActions actions)
        {
            this.actions = actions;
        }
        public IViewComponentResult Invoke()
        {
            return View(actions.GetContactList());   
        }
    }
}
