import { Temporal } from "@js-temporal/polyfill";

export interface Course {
readonly id: string;
title: string;
capacity: number;
startDate?: Temporal.PlainDate;
}

export type CourseStatus =
| { status: "DRAFT"; createdBy: string; createdAt: Temporal.Instant }
| { status: "PUBLISHED"; publishedAt: Temporal.Instant; syllabus: string }
| {status: "ACTIVE"; enrolledCount: number; startDate: Temporal.PlainDate;}
| {status: "ARCHIVED"; archivedAt: Temporal.Instant; finalEnrollmentCount: number;}
| {status: "CANCELLED"; reason: string; cancelledAt: Temporal.Instant };

export function describeCourse(course: CourseStatus): string {
switch (course.status) {
case "DRAFT":   
    return `Draft created by ${course.createdBy} at ${course.createdAt}`;
case "PUBLISHED":
    return `Published at ${course.publishedAt} with syllabus: ${course.syllabus}`;
case "ACTIVE":
    return `Active since ${course.startDate} with ${course.enrolledCount} students enrolled`;
case "ARCHIVED":
    return `Archived at ${course.archivedAt} with final enrollment count: ${course.finalEnrollmentCount}`;
case "CANCELLED":
    return `Cancelled: ${course.reason} at ${course.cancelledAt}`;
default: {
  const _check: never = course;
  throw new Error(`Unhandled status: ${JSON.stringify(_check)}`);
        }
    }
}