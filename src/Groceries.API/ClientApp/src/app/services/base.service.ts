import { throwError } from "rxjs";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { catchError, map } from "rxjs/operators";
import { AppError, BadInput } from "./AppError";
import { environment } from "../../environments/environment";


export class BaseService {

  private url: string;

  constructor(private http: HttpClient) {
    this.url = environment.apiBase
  }

  get(uri: string) {
    return this.http.get(this.url + uri).pipe(
      catchError(this.handleError));
  }

  post(uri: string, resource: any) {
    return this.http.post(this.url + uri, resource, { reportProgress: true, observe: 'events' }).pipe(
      catchError(this.handleError)
    );
  }

  put(uri: string, id: any, resource: any) {
    return this.http.put(this.url + uri + id, resource).pipe(
      catchError(this.handleError)
    );
  } 

  private handleError(error: Response) {
    if (error.status === 400) {
      return throwError(new BadInput(error));
    }
    return throwError(new AppError(error));
  }
}
