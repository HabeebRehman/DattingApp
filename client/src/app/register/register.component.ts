import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '_Services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model:any={};
  // @Input() userFromHomeComponent:any;
  @Output() CancelRegister=new EventEmitter();

  constructor(private accountService: AccountService,private toaster:ToastrService) { }

  ngOnInit(): void {
  }

  register()
  {
    console.log(this.model)
    this.accountService.Register(this.model).subscribe(resp=>{
      console.log(resp);
      this.cancel();
    },error=>{
      console.log(error);
      this.toaster.error(error.error);

    });
    
  }

  cancel()
  {
    this.CancelRegister.emit(false);
  }



}
