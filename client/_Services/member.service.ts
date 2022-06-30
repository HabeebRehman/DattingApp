import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '_Model/members';



@Injectable({
  providedIn: 'root'
})

export class MemberService {

  baseUrl= environment.apiUrl

  constructor(private http: HttpClient) { }

 
  getMembers()
  {
       console.log(" Enter to Member")
  
       return this.http.get<Member[]>(this.baseUrl+'users')
 
  }

  getMember(userName:string)
  {
    
    return this.http.get<Member>(this.baseUrl+'users/'+userName)
  }
}
