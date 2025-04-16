import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { BoatsItem } from '../boats-datasource';
import { CommonModule } from '@angular/common';

export interface BoatDeleteDialogData {
  boat: BoatsItem;
}

@Component({
  selector: 'app-boat-delete-dialog',
  imports: [MatDialogModule, MatButtonModule, CommonModule],
  templateUrl: './boat-delete-dialog.component.html',
  styleUrl: './boat-delete-dialog.component.scss'
})
export class BoatDeleteDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: BoatDeleteDialogData) {}
}
