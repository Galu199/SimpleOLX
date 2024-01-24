export const allAdvertsCategories = ['Electronics', 'Sport', 'Music', 'Education', 'Health'] as const
export type AdvertCategory = (typeof allAdvertsCategories)[number]