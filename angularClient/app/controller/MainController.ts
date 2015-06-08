module dk.kalleguld.AngularChatt {
    export class MainController {

        userService: IUserService;
        messageService: IMessageService;

        static $inject = [
            "dk.kalleguld.AngularChatt.IUserService",
            "dk.kalleguld.AngularChatt.IMessageService"];
        constructor(us: IUserService, ms: IMessageService) {
            this.userService = us;
            this.messageService = ms;
        }



    }
}