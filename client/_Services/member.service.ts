import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '_Model/members';



@Injectable({
  providedIn: 'root'
})

export class MemberService {

  baseUrl= environment.apiUrl
  memeber:Member[]=[];
  constructor(private http: HttpClient) { }

 
  getMembers()
  {
   if(this.memeber.length>0)
   return of(this.memeber)
   return this.http.get<Member[]>(this.baseUrl+'users').pipe(
    map(members=>{
      this.memeber=members;
      return members;
    }
    )
    
   )
 
  }

  getMember(userName:string)
  {
    const member=this.memeber.find(x=>x.userName==userName);
    if(member !==undefined)
    {
      return of(member)
    }
    return this.http.get<Member>(this.baseUrl+'users/'+userName)
  }

  UpdateMember(member:Member)
  {
    return this.http.put<Member>(this.baseUrl+'users/',member).pipe(
      map(()=>{
        const index=this.memeber.indexOf(member);
        this.memeber[index]=member;
      })
    )
  }

  SetMainPhot(PhotoID:number)
  {
    return this.http.put(this.baseUrl+'users/Set-Main-Photo/'+PhotoID,{});
  }

  DeletePhoto(PhotoID:number)
  {
    return this.http.delete(this.baseUrl+'users/Delete-Photo/'+PhotoID)
  }

}
