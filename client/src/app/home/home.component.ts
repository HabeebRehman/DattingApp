import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  RegisterMode:Boolean=false
  users:any;

  constructor(private http:HttpClient ) { }

  ngOnInit(): void {
   // this.getUsers()
  }

  registerToggle()
  {
    this.RegisterMode=!this.RegisterMode
  }

  // getUsers()
  // {
  //   this.http.get('https://localhost:5001/api/users').subscribe(users=>this.users=users);
  // }

  CancelRegistration(event:boolean)
  {
    this.RegisterMode=event;
  }

}
