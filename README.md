# 🛠️ Inventory System: Skeuomorphic Project & Inventory Manager

Inventory System is a desktop inventory and productivity application designed to catalog projects, tools, and materials. Built with a heavy appreciation for early-2000s design principles, the application features a fully custom, toggleable skeuomorphic user interface inspired by the iconic Windows XP era—bringing tactile, rich visual design back to modern utility software.



![Application Screenshot](path/to/your/screenshot.png) <!-- 💡 Tip: Add a screenshot or GIF of your UI here! -->

## 🚀 Features

### **Current Functionality (MVP)**
* **Active Project Tracker:** Full CRUD functionality to create, update, and manage ongoing hardware and software development projects. Here you can also add what components and parts that you need to purchase for the project.
* **Skeuomorphic UI Engine:** Custom-styled windows, borders, and controls that replicate a vintage desktop aesthetic without relying on standard modern flat-design frameworks.
* **Contextual ToolTips & Hints:** Retro-inspired user assistance and micro-interactions built entirely from scratch.
* **Shopping List Builder:** This makes a PDF based on what you have in the Bill of Materials of each project and what you have logged in the database that you own. As of 7/12/2026 you cannot add components that you own that aren't directly related to a project. This functionality is coming. 
* 7/12/2026: ONLY the Projects and the Shopping List sections have functionality. I have started to work on the Components & Parts section. I will update the code inm this repository and update this READ.ME as I go. The header images are also placeholders and will be fixed in a future revision.

### **On the Roadmap**
* [ ] **Tool Cataloging System:** Dedicated database module to track physical tool location, calibration dates, and usage history.
* [ ] **Material & Component Ledger:** Inventory tracking for hardware components (e.g., microcontrollers, capacitors, fasteners) linked directly to project requirements.

---

## 🛠️ Tech Stack

* **Language:** C#
* **Framework/GUI:** .NET and WinForms
* **Database/Storage:** MS SQL 
* **IDE:** Visual Studio 2022

---

## 🧠 Architectural & Engineering Challenges

### **Custom UI Rendering & Skeuomorphism**
Modern development tools are heavily optimized for flat UI design. Replicating a tactile, early-2000s Windows XP aesthetic required bypassing standard component styles and overriding native paint events. This involved managing pixel-perfect layouts, implementing custom bitmap drawing routines, and manually handling control states (hover, active, disabled) to ensure a seamless, high-fidelity retro experience.

### **Data Architecture**
Designed a relational, lightweight data structure to cleanly separate the application's presentation layer from the core business logic. This ensures that as the tool and material inventory modules are introduced, they can easily hook into the existing project tracking schema.

---

## ⚙️ Getting Started

### Prerequisites
* Visual Studio 2022 or newer
* MS SQL Server 2019 or newer
