import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '_Model/User';
import { AccountService } from '_Services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  CurrentUser:User;
  model:any ={};
  color="black";
 
  
  constructor(public accountService:AccountService) { }

  ngOnInit(): void {
  
  }

  login()
  {
    this.accountService.login(this.model).subscribe
    (Response=>
      {
        console.log(Response);
      

      },error=>console.log(error)
    );
   
    
  }

  logout()
  {
    this.accountService.logout();
  }

  

}
