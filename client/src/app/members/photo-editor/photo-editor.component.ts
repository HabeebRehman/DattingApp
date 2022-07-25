import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '_Model/members';
import { Photo } from '_Model/photo';
import { User } from '_Model/User';
import { AccountService } from '_Services/account.service';
import { MemberService } from '_Services/member.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() member:Member
  uploader:FileUploader
  hasBaseDropZoneOver=false
  baseUrl=environment.apiUrl
  user:User
  constructor(private accountService:AccountService,private memberService:MemberService) {

    this.accountService.CurrentUser$.pipe(take(1)).subscribe(user=>this.user=user)
   }
  
  InitializeUploader()
  {
    this.uploader=new FileUploader ({
      url:this.baseUrl+'users/add-photo',
      authToken:'Bearer '+this.user.token,
      isHTML5:true,
      allowedFileType:['image'],
      removeAfterUpload:true,
      autoUpload:false,
      maxFileSize:10*1024*1024
      
    })

    this.uploader.onAfterAddingFile=(file)=>
    {
      file.withCredentials=false;
    }
    
    this.uploader.onSuccessItem=(item,response,status,headers)=>{
      if(response)
      {
        const photo=JSON.parse(response)
        this.member.photos.push(photo)
      }
    }
    

  }
 
 fileOverBase(e:any)
 {
  this.hasBaseDropZoneOver=e;

 }

 SetMaiPhoto(Photo:Photo)
 {
     this.memberService.SetMainPhot(Photo.id).subscribe
     (()=>{
       this.user.photoUrl=Photo.url;
       this.accountService.setCurrentUser(this.user);
       this.member.photoUrl=Photo.url;
       this.member.photos.forEach(p=>{

         if(p.isMain) p.isMain=false;
         if(p.id==Photo.id)p.isMain=true;

       })
     })
 }

 DeletePhoto(PhotoID:number)
 {
    this.memberService.DeletePhoto(PhotoID).subscribe(()=>{

      this.member.photos=this.member.photos.filter(x=>x.id !=PhotoID);

    })
 }



  ngOnInit(): void {
    this.InitializeUploader();
  }

}
