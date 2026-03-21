<div style = "font-family: 'Roboto', sans-serif;">

### souqna.v0

- Adopted **Layered Architecture** to enable rapid market entry while maintaining a clear separation of concerns across **Business**, **Infrastructure**, and **Presentation** layers, ensuring the codebase remains extensible for future versions.
- Implemented core e-commerce workflows allowing users to browse and discover products, manage their cart, place orders, and complete payments via the **Paymob** payment gateway integration.
- Achieved **82%** code coverage through **37** comprehensive unit tests using **xUnit**, **Moq**, and **FluentAssertions** across all core e-commerce workflows.
- To view the codebase for the `souqna.v0` version, run: `git checkout souqna.v0`

### souqna.v1

- Migrated from a **Layered Architecture** to a **Clean Architecture**, enforcing the dependency rule to decouple core business logic from external frameworks. By isolating the **Domain** and **Application** layers, the system now achieves a high degree of persistence ignorance and testability, ensuring the core domain remains agnostic of infrastructure volatility and presentation-layer shifts.
- Implemented inventory management with optimistic concurrency control using versioning, resolving high-concurrency race conditions during simultaneous checkout attempts to ensure data integrity, stock accuracy, and consistent order processing.
- Developed a complete **React.js** client-side application to deliver a dynamic and responsive user experience, leveraging AI-assisted development (vibe coding) within modern IDEs like **Trae** and **Antigravity** to accelerate UI implementation while maintaining high code quality and responsiveness.

</div>