﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AITIssueTracker.API.v0._2_Manager;
using AITIssueTracker.API.v0._2_Manager.Contracts;
using AITIssueTracker.Model.v0;
using AITIssueTracker.Model.v0._3_ViewModel;
using AITIssueTracker.Model.v0._1_FormModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AITIssueTracker.API.v0._1_Controller
{
    [ApiController]
    [ApiVersion("0.0")]
    [Route(Endpoints.BASE_PROJECT)]
    [SwaggerTag("Manage projects and its developers")]
    public class ProjectController : ControllerBase
    {
        private IProjectService Service { get; }

        public ProjectController(IProjectService service)
        {
            Service = service;
        }

        /// <summary>
        /// Get all existing projects.
        /// </summary>
        /// <param name="filter"></param>
        [HttpGet]
        public async Task<IActionResult> GetAllProjectsAsync(
            [FromQuery] string filter)
        {
            List<ProjectView> allProjects = await Service.GetProjectsAsync(filter);
            return allProjects is null ? NotFound() : Ok(allProjects);
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject"></param>
        [HttpPost]
        public async Task<IActionResult> PostNewProjectAsync(
            [FromBody] ProjectForm newProject)
        {
            ProjectView savedProject = await Service.SaveProjectAsync(newProject);

            return savedProject is null ? BadRequest() : Ok(savedProject);
        }

        /// <summary>
        /// Delete an existing project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{projectId:guid}")]
        public async Task<IActionResult> DeleteProjectAsync(
            [FromRoute] Guid projectId)
        {
            bool isDeleted = await Service.DeleteProjectAsync(projectId);

            return !isDeleted ? BadRequest() : Ok();
        }

        /* === Extended User interactions === */

        /// <summary>
        /// Adds a user (developer) to a project.
        /// </summary>
        /// <param name="userToProjectForm"></param>
        [HttpPost]
        [Route("user")]
        public async Task<IActionResult> PostUserToProjectAsync(
            [FromBody] ProjectUserForm userToProjectForm)
        {
            if (!await Service.AddUserToProjectAsync(userToProjectForm.Username, userToProjectForm.ProjectId))
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Removes a user (developer) from a project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="username"></param>
        [HttpDelete]
        [Route("user/{projectId:guid}/{username}")]
        public async Task<IActionResult> DeleteUserFromProjectAsync(
            [FromRoute] Guid projectId,
            [FromRoute] string username)
        {
            if (!await Service.RemoveUserFromProjectAsync(username, projectId))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}