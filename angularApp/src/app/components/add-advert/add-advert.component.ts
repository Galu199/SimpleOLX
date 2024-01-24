import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Advert } from '../../model/interfaces';
import { AdvertService } from '../../services/advert/advert.service';

@Component({
  selector: 'app-add-advert',
  templateUrl: './add-advert.component.html',
  styleUrls: ['./add-advert.component.scss']
})
export class AddAdvertComponent implements OnInit {

  selectedImages: File[] = [];
  newAdvert: Advert | undefined;

  constructor(private formBuilder: FormBuilder,
              private advertService: AdvertService,
  ) { }

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
  onFileChange(event: any): void {
    const files: FileList = event.target.files;
    // Limit to 10 images
    for (let i = 0; i < files.length && i < 10; i++) {
      this.selectedImages.push(files[i]);
    }
    // Display thumbnails
    this.updateThumbnailDisplay();
  }

  // Function to remove a selected image
  removeImage(index: number): void {
    this.selectedImages.splice(index, 1);
    // Update display after removal
    this.updateThumbnailDisplay();
  }

  // Function to update thumbnail display
  private updateThumbnailDisplay(): void {
    const imageUrls: string[] = this.selectedImages.map(image => window.URL.createObjectURL(image));
    this.addAdvert.patchValue({ images: imageUrls }); // Update form control with image URLs
  }

  add() {
    const advert: Advert = {
        title: this.addAdvert.value.title,
        description: this.addAdvert.value.description,
        mail: "hh",
        phoneNumber: "string",
        price: this.addAdvert.value.price,
        localizationLatitude: this.addAdvert.value.localization,
        localizationLongitude: 1,
        category: this.addAdvert.value.category,
        image: "string",
        userOwnerId: 1
    }
    this.advertService.postAdvert(advert);
    console.log(advert);
  }
}

