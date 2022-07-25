import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from '_Model/members';
import { Photo } from '_Model/photo';
import { User } from '_Model/User';
import { AccountService } from '_Services/account.service';
import { MemberService } from '_Services/member.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
@ViewChild('editForm') editForm:NgForm;
@HostListener('window:beforeunload',['$event']) UnloadNotification($event:any)
{
  if(this.editForm.dirty)
  {
    $event.returnValue=true;
  }
}

member:Member;
user:User;

  constructor(private memberService:MemberService,private accountService:AccountService,private toaster:ToastrService) { 

    this.accountService.CurrentUser$.pipe(take(1)).subscribe(
      user=>{
        this.user=user
        console.log("getted user name "+user.userName)
      }
    )
  }

  ngOnInit(): void {
   
    
    this.loadMember()

  }

  loadMember()
  {
    this.memberService.getMember(this.user.userName).subscribe(member=>{
      this.member=member
      console.log("getted user name "+this.member.photoUrl)
    })
  }

  updateMember()
  {
    this.memberService.UpdateMember(this.member).subscribe(
      ()=>{
        this.toaster.success("profile updated successfully");
        this.editForm.reset(this.member);
      }
    )
   
  }




}
