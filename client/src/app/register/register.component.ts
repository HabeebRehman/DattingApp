import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '_Services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model:any={};
  @Input() userFromHomeComponent:any;
  @Output() CancelRegister=new EventEmitter();

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register()
  {
    console.log(this.model)
    this.accountService.Register(this.model).subscribe(resp=>{
      console.log(resp);
      this.cancel();
    },error=>console.log(error));
    
  }

  cancel()
  {
    this.CancelRegister.emit(false);
  }



}
