import customtkinter as ctk
from astral import LocationInfo
from astral.sun import sun
from datetime import datetime, timedelta


# ====================== Astral Functionality ======================

# Define location information
Location = LocationInfo("Cairo", "Egypt", "Africa/Cairo", latitude=30, longitude=31)

# Calculate sunset
today = datetime.now().date()
s = sun(Location.observer, date=today)
sunset = s["sunset"]
sunset = sunset + timedelta(hours=2)  # Adjust sunset time
midnight = datetime.combine(today, datetime.min.time())
sunset_as_midnight = sunset - sunset.replace(hour=0, minute=0, second=0)

# Function to adjust time based on the night base
def Adjust_time_to_night_base(time: datetime):
    global sunset
    sunset = sunset.replace(tzinfo=None)
    if time < sunset:
        time += timedelta(days=1)

    adjust_time = time + sunset_as_midnight
    return adjust_time


# ====================== CustomTkinter GUI ======================

# Create main window
app = ctk.CTk()
app.title("برنامج الساعة")
# app.geometry("500x300")

# Define styles
font_style = ctk.CTkFont(family="Helvetica", size=36, weight="bold")

# Define Frames
original_time_frame = ctk.CTkFrame(app, fg_color="transparent")
adjusted_time_frame = ctk.CTkFrame(app, fg_color="transparent")

original_time_frame.pack(padx=15, pady=15)
adjusted_time_frame.pack(padx=15, pady=15)

# Create labels

# Original labels
original_time_label = ctk.CTkLabel(original_time_frame, text="التوقيت الشمسي المعتاد = ", font=font_style)
original_time_label.grid(column=0, row=0)

original_time_clock = ctk.CTkLabel(original_time_frame, text="", font=font_style)
original_time_clock.grid(column=1, row=0)

# adjusted labels
adjusted_time_label = ctk.CTkLabel(adjusted_time_frame, text="التوقيت القـــمـــــــري = ", font=font_style)
adjusted_time_label.grid(column=0, row=0)

adjusted_time_clock = ctk.CTkLabel(adjusted_time_frame, text="", font=font_style)
adjusted_time_clock.grid(column=1, row=0)


# Update clocks dynamically
def update_clocks():
    # Original time
    example_time = datetime.now()
    adjust_time = Adjust_time_to_night_base(example_time)

    # Update labels
    original_time_clock.configure(text=f"{example_time.strftime('%d/%m/%Y - %I:%M:%S %p')}")
    adjusted_time_clock.configure(text=f"{adjust_time.strftime('%d/%m/%Y - %I:%M:%S %p')}")

    # Call update function again after 1 second
    app.after(1000, update_clocks)

# Initialize the clock update
update_clocks()

# Run the application
app.mainloop()
