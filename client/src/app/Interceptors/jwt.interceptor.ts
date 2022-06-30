import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '_Services/account.service';
import { User } from '_Model/User';
import { take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private AccountService:AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
   let currentUser:User
   this.AccountService.CurrentUser$.pipe(take(1)).subscribe(user=>currentUser=user);
   if(currentUser)
   {
    request=request.clone(
    {
      setHeaders:
      {
        Authorization:`Bearer ${currentUser.token}`
      }
    }
    );
  
   }

    return next.handle(request);
  }
}
