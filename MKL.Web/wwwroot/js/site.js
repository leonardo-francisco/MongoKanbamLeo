document.addEventListener('DOMContentLoaded', function () {
    var projectList = [];
    var selectedProjectId = null;

    async function fetchProjects() {
        try {
            const response = await fetch('/Kanban/GetAllProjects');
            if (response.ok) {
                projectList = await response.json();
                refreshProjectSelect();
            } else {
                console.error('Failed to fetch projects');
            }
        } catch (error) {
            console.error('Error fetching projects:', error);
        }
    }

    function refreshProjectSelect() {
        var projectSelect = document.getElementById('projectSelect');
        projectSelect.innerHTML = ''; // Clear existing options
        projectList.forEach(project => {
            var option = document.createElement('option');
            option.value = project.id;
            option.textContent = project.name;
            projectSelect.appendChild(option);
        });
    }  

    document.getElementById('projectSelect').addEventListener('change', function () {
        selectedProjectId = this.value;
        var projectName = this.options[this.selectedIndex].textContent;
        if (selectedProjectId) {
            document.querySelector('#kanbanContainer span').textContent = projectName;
            document.getElementById('kanbanContainer').style.display = 'block';
        } else {
            document.getElementById('kanbanContainer').style.display = 'none';
        }
    });

    // Fetch projects on page load
    fetchProjects();
});

