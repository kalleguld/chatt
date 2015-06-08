module dk.kalleguld.AngularChatt {
    export class RerestService implements IRerestService {

        static baseUrl = "http://localhost:8733/jsonv1/";

        getUrl(base: string, params: any): string {
            var result = RerestService.baseUrl;
            result += base;
            var first = true;
            for (var p in params) {
                result += (first ? "?" : "&");
                first = false;

                result += p + "=" + params[p];
            }
            return result;
        }
    }
}