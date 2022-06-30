import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { User } from '_Model/User';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  
  baseUrl:string=environment.apiUrl;
  private CurrentUserSource=new ReplaySubject<User>(1);
  CurrentUser$= this.CurrentUserSource.asObservable();

  constructor(private http:HttpClient) { }



  
  login(model:User)
  {
      return this.http.post(this.baseUrl+'Account/Login',model).pipe(map((response:any)=>{
        const user=response;
        if(user)
        {
          localStorage.setItem('user',JSON.stringify(user));
          this.CurrentUserSource.next(user);
        }
      })
      )
        
  }

  Register(model:User)
  {
    console.log(model.UserName);
    console.log(model.Password);
    return this.http.post(this.baseUrl+'Account/register',model).pipe(map
      ((response:any)=>{
      const user=response;
      if(user)
      {
        localStorage.setItem('user',JSON.stringify(user));
        this.CurrentUserSource.next(user)
      }
      return user;
    }))

    
  }

  logout()
  {
    localStorage.removeItem('user');
    this.CurrentUserSource.next(null);

  }
  setCurrentUser(user:User)
  {
      this.CurrentUserSource.next(user);
  }

}
