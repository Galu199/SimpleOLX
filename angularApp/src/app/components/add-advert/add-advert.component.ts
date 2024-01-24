import { AuthService } from 'src/app/services/auth-service/auth.service'
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Advert } from '../../model/interfaces';
import { AdvertService } from '../../services/advert/advert.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AdvertCategory, allAdvertsCategories } from 'src/app/model/types';

@Component({
  selector: 'app-add-advert',
  templateUrl: './add-advert.component.html',
  styleUrls: ['./add-advert.component.scss']
})
export class AddAdvertComponent implements OnInit {

  previews: string[] = []
  selectedFiles?: FileList
  selectedFileNames: string[] = []

  newAdvert: Advert | undefined;
  allAdvertsCategories: AdvertCategory[] = [...allAdvertsCategories]

  constructor(private formBuilder: FormBuilder,
              private advertService: AdvertService,
              private snackBar: MatSnackBar,
              private authService: AuthService) { }

  ngOnInit(): void {
  }

  addAdvert = this.formBuilder.group({
    title: ['', [Validators.required, Validators.minLength(16)]],
    description: ['', [Validators.required, Validators.minLength(40)]],
    category: ['', [Validators.required]],
    image: ['', [Validators.required]],
    localization: ['', [Validators.required, Validators.minLength(4)]],
    price: ['', [Validators.required]],
  });

  // Function to handle file input change
  selectFiles(event: any): void {
    this.selectedFiles = event.target.files
    
    if (this.selectedFiles && this.selectedFiles[0]) {
      const numberOfFiles = this.selectedFiles.length

      for (let i = 0; i < numberOfFiles; i++) {
        const reader = new FileReader();

        reader.onload = (e: any) => { this.previews.push(e.target.result) }

        reader.readAsDataURL(this.selectedFiles[i]);

        this.selectedFileNames.push(this.selectedFiles[i].name);
      }
    }
    //this.updateThumbnailDisplay();
  }

  // Function to remove a selected image
  removeImage(index: number): void {
    this.previews.splice(index, 1);
    this.selectedFileNames.splice(index, 1);

    // Update display after removal
    //this.updateThumbnailDisplay();
  }

  // Function to update thumbnail display
  //private updateThumbnailDisplay(): void {
    //const imageUrls: string[] = this.selectedFileNames.map(image => window.URL.createObjectURL(image));
    //this.addAdvert.patchValue({ images: this.selectedFileNames }); // Update form control with image URLs
  //}

  add() {
    const advert: Advert = {
        title: this.addAdvert.value.title,
        description: this.addAdvert.value.description,
        mail: "hh",
        phoneNumber: "string",
        price: this.addAdvert.value.price,
        localizationLatitude: 1, //float
        localizationLongitude: 1, //float
        category: this.addAdvert.value.category,
        image: this.selectedFiles![0],
        userOwnerId: this.authService.getUserId()
    }
    this.advertService.postAdvert(advert).subscribe({
      next: (value: string) => {
        this.snackBar.open(value, 'Close', { duration: 2000, horizontalPosition: 'right', verticalPosition: 'top' })
      },
      error: (err: HttpErrorResponse) => { console.log(err.message); }
    });

    console.log(advert);
  }
}

