import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoatDeleteDialogComponent } from './boat-delete-dialog.component';

describe('BoatDeleteDialogComponent', () => {
  let component: BoatDeleteDialogComponent;
  let fixture: ComponentFixture<BoatDeleteDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoatDeleteDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoatDeleteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
