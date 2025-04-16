import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoatEditCreateDialogComponent } from './boat-edit-create-dialog.component';

describe('BoatEditCreateDialogComponent', () => {
  let component: BoatEditCreateDialogComponent;
  let fixture: ComponentFixture<BoatEditCreateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoatEditCreateDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoatEditCreateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
