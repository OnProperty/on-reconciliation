describe("Foo", () => {

    beforeEach(() => {
        // login logic here, after auth is implemented
    });

    it("can click on buttons", () => {
        cy.visit(`${Cypress.env("baseUrl")}/counter`);
        cy.getByDataCy("incrementer").should('be.visible').click();
        cy.wait(1000);
        cy.getByDataCy("counter").should('have.text', "1")
    });
});