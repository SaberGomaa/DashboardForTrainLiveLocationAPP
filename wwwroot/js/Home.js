
const hideButton = document.getElementById('menuicn');
const spans = document.querySelectorAll('#allspan span');

let isHidden = false;


let menuicn = document.querySelector(".menuicn");
let nav = document.querySelector(".navcontainer");

menuicn.addEventListener("click", () => {
    nav.classList.toggle("navclose");
    if (isHidden) {
        spans.forEach(span => {
            span.style.display = 'inline'; // or 'inline' or their original display value
        });
        hideButton.textContent = 'Hide All'; // update the button label
    } else {
        spans.forEach(span => {
            span.style.display = 'none';
        });
        hideButton.textContent = 'Show All'; // update the button label
    }
    isHidden = !isHidden; // toggle the state flag
})

