import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
  baseUrl='https://localhost:5001/api/';
  validationError:string[]=[];
  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }

  get404Error()
  {
    this.http.get(this.baseUrl+'Buggy/not-found').subscribe(resp=>{
      console.log(resp);
    },err=>{console.log(err)});
    
  }
  get400Error()
  {
    this.http.get(this.baseUrl+'Buggy/bad-request').subscribe(resp=>{
      console.log(resp);
    },err=>{console.log(err)});
    
  }
  get500Error()
  {
    this.http.get(this.baseUrl+'Buggy/server-error').subscribe(resp=>{
      console.log(resp);
    },err=>{
      console.log("New Error"+err)
    });
    
  }
  get401Error()
  {
    this.http.get(this.baseUrl+'Buggy/auth').subscribe(resp=>{
      console.log(resp);
    },err=>{

      console.log(err)
    
    });
    
  }
  get400ValidationError()
  {
    this.http.post(this.baseUrl+'account/Register',{}).subscribe(resp=>{
      console.log(resp);
    },err=>{
      console.log(err)
      this.validationError=err
    
    }
    
    );
    
  }

}