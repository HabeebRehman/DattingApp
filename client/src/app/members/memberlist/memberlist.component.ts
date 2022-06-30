import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from '_Model/members';

import { MemberService } from '_Services/member.service';

@Component({
  selector: 'app-memberlist',
  templateUrl: './memberlist.component.html',
  styleUrls: ['./memberlist.component.css']
})

export class MemberlistComponent implements OnInit {

  member:Member[]
  constructor(private memberService:MemberService) { }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers()
  {
 
    this.memberService.getMembers().subscribe(member=>{this.member=member})
 
  }

}
