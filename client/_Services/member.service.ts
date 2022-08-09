import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { PaginationResult } from '_Model/Pagination';
import { environment } from 'src/environments/environment';
import { Member } from '_Model/members';
import { UserParams } from '_Model/UserParams';
import { AccountService } from './account.service';
import { User } from '_Model/User';



@Injectable({
  providedIn: 'root'
})

export class MemberService {

  baseUrl= environment.apiUrl
  memeber:Member[]=[];
  user:User
  userParams:UserParams
  memberCache=new Map();

  constructor(private http: HttpClient, private accountService: AccountService) { 
    this.accountService.CurrentUser$.pipe(take(1)).subscribe(user=>{
      this.user=user;

      this.userParams=new UserParams(user);

    });
 }
 
 
  getMembers(UserParams:UserParams)
  {
     // if(this.memeber.length>0)
     
     var respons=this.memberCache.get(Object.values(UserParams).join('-'));
     if(respons)
     {
        return of(respons);
     }
    let params= this.getPaginationHeaders(UserParams.PageNumber,UserParams.PageSize)
    params=params.append('MinAge',UserParams.MinAge.toString());
    params=params.append('MaxAge',UserParams.MaxAge.toString());
    params=params.append('Gender',UserParams.Gender);
    params=params.append('OrderBy',UserParams.OrderBy);


     // return of(this.memeber)

      return this.getPaginationResult<Member[]>(this.baseUrl + 'users',params).pipe(map(respons=>{
        this.memberCache.set(Object.values(UserParams).join('-'),respons)
        return respons;
      }))
 
  }

  private getPaginationResult<T>(url:string,params: HttpParams) {
    const paginatedResult:PaginationResult<T>=new PaginationResult<T>();
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.Result = response.body;
        if (response.headers.get("Pagination") !== null) {
          paginatedResult.Pagination = JSON.parse(response.headers.get("Pagination"));
        }
        return paginatedResult;
      }
      )

    );
  }

  private getPaginationHeaders(PageNumber:number,PageSize:number)
  {
    let params=new HttpParams();
    params=params.append('PageNumber',PageNumber.toString());
    params=params.append('PageSize',PageSize.toString());
    return params;
  }

  getMember(userName:string)
  {
    
    // const member=this.memeber.find(x=>x.userName==userName);
    // if(member !==undefined)
    // {
    //   return of(member)
    // }
    console.log(userName);
    console.log(this.memberCache);
    const member=[...this.memberCache.values()].reduce((arr,ele)=>arr.concat(ele.Result),[]).find((member:Member)=>member.userName===userName)
    if(member)
    {
      return of(member);
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

  getUserParams()
  {
    return this.userParams;
  }

  setUserParams(params:UserParams)
  {
      this.userParams=params;
  }
  ResetUserParams()
  {
      this.userParams=new UserParams(this.user);
      return this.userParams;
  }
}
