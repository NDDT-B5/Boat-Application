import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { BoatItem } from '../../core/models/boat.model';

export interface BoatDeleteDialogData {
  boat: BoatItem;
}

@Component({
  selector: 'app-boat-delete-dialog',
  imports: [MatDialogModule, MatButtonModule, CommonModule],
  templateUrl: './boat-delete-dialog.component.html',
  styleUrl: './boat-delete-dialog.component.scss'
})
export class BoatDeleteDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: BoatDeleteDialogData, 
    private dialogRef: MatDialogRef<BoatDeleteDialogComponent>
  ) {}

  onDelete() {
    this.dialogRef.close(true);
  }
}
