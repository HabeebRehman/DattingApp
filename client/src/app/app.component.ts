import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {User} from '_Model/User'
import { AccountService } from '_Services/account.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Datting App';
  users:any
  constructor(private AccountService:AccountService)
  {}
  ngOnInit() {
    
    this.setCurrentUser();
  }

  

  setCurrentUser()
  {
    const User:User=JSON.parse(localStorage.getItem('user'));
    this.AccountService.setCurrentUser(User);
  }
}
