import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Pagination } from '_Model/Pagination';
import { Member } from '_Model/members';

import { MemberService } from '_Services/member.service';
import { AccountService } from '_Services/account.service';
import { UserParams } from '_Model/UserParams';
import { User } from '_Model/User';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-memberlist',
  templateUrl: './memberlist.component.html',
  styleUrls: ['./memberlist.component.css']
})

export class MemberlistComponent implements OnInit {

  Members:Member[];
  Pagination:Pagination;
  userParams:UserParams;
  user:User;
  GenderList=[{value:'male',display:'Males'},{value:'female',display:'Females'}]



   constructor(private memberService:MemberService,private accountService:AccountService) {

    this.userParams=this.memberService.getUserParams();

   }

  ngOnInit(): void {
   // this.member$=this.memberService.getMembers();
   this.loadMembers()
  }

  loadMembers()
  {
    this.memberService.setUserParams(this.userParams);
    this.memberService.getMembers(this.userParams).subscribe(
      respons=>{
        this.Members=respons.Result;
        this.Pagination=respons.Pagination;
        
      }
    )
  }

  PageChanged(event:any)
  {
      this.userParams.PageNumber=event.page;
      this.memberService.setUserParams(this.userParams);
      this.loadMembers();
  }

  resetFilter()
  {
    this.userParams=this.memberService.ResetUserParams();
    this.loadMembers();
  }

}
